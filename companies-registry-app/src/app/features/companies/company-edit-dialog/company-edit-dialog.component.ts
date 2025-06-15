import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Client, CompanyResponse } from '../../../api/api-reference';

@Component({
  selector: 'app-company-edit-dialog',
  standalone: true,
  templateUrl: './company-edit-dialog.component.html',
  styleUrls: ['./company-edit-dialog.component.scss'],
  imports: [
    CommonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule
  ]
})
export class CompanyEditDialogComponent {
  form: FormGroup;
  isSaving = false;
  isEditMode: boolean;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CompanyEditDialogComponent>,
    private client: Client,
    @Inject(MAT_DIALOG_DATA) public data: { company?: CompanyResponse, isEdit: boolean }
  ) {
    this.isEditMode = data.isEdit;
    const company = data.company;
    
    this.form = this.fb.group({
      name: [company?.name || '', Validators.required],
      exchange: [company?.exchange || '', Validators.required],
      ticker: [company?.ticker || '', Validators.required],
      isin: [company?.isin || '', Validators.required],
      webSite: [company?.webSite || '']
    });
  }

  onSubmit(): void {
    if (!this.form.valid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSaving = true;
    
    if (this.isEditMode) {
      // Edit existing company
      this.client.companiesPUT(this.data.company!.id!, this.form.value).subscribe({
        next: () => {
          this.dialogRef.close(true); // Close with success result
        },
        error: (error) => {
          this.handleError(error);
        }
      });
    } else {
      // Create new company
      this.client.companiesPOST(this.form.value).subscribe({
        next: () => {
          this.dialogRef.close(true); // Close with success result
        },
        error: (error) => {
          this.handleError(error);
        }
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }

  handleError(error: any) {
    this.isSaving = false;
    console.error('Caught error:', error);
    
    // NSwag throws an ApiException with a .response string (JSON)
    if (error instanceof Error && 'response' in error) {
      try {
        const parsed = JSON.parse((error as any).response);
        const validationErrors = parsed?.errors;
        if (validationErrors && typeof validationErrors === 'object') {
          this.applyServerValidationErrors(validationErrors);
          return;
        }
      } catch (parseErr) {
        console.error('Failed to parse response JSON:', parseErr);
      }
    }
    
    // fallback if not a validation error
    console.error('Save failed', error);
  }

  private applyServerValidationErrors(errors: Record<string, string[]>) {
    for (const serverField in errors) {
      const clientField = serverField.charAt(0).toLowerCase() + serverField.slice(1);
      const control = this.form.get(clientField);
      if (control) {
        control.setErrors({ serverError: errors[serverField][0] });
        control.markAsTouched();
      }
    }
  }
}