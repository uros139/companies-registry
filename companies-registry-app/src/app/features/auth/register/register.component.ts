import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Client, RegisterUserCommand } from '../../../api/api-reference';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  registerForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
  });

  error = '';

  constructor(
    private fb: FormBuilder,
    private client: Client,
    private router: Router
  ) { }

onSubmit() {
  if (this.registerForm.invalid) {
    this.registerForm.markAllAsTouched();
    return;
  }
  const command = new RegisterUserCommand({
    firstName: this.registerForm.value.firstName!,
    lastName: this.registerForm.value.lastName!,
    email: this.registerForm.value.email!,
    password: this.registerForm.value.password!,
  });

  this.client.register(command).subscribe({
    next: () => this.router.navigate(['/login']),
    error: (err) => this.handleError(err),
  });
}

  handleError(error: any) {
    console.error('Caught error:', error);

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

    // fallback error message
    this.error = 'Registration failed. Please try again.';
  }

  private applyServerValidationErrors(errors: Record<string, string[]>) {
    for (const serverField in errors) {
      const clientField = serverField.charAt(0).toLowerCase() + serverField.slice(1);
      const control = this.registerForm.get(clientField);
      if (control) {
        control.setErrors({ serverError: errors[serverField][0] });
        control.markAsTouched();
      }
    }
  }
}
