import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { CompanyResponse } from '../../../../api/api-reference';
import { CompanyEditDialogComponent } from '../company-edit-dialog.component';

@Injectable({ providedIn: 'root' })
export class CompanyDialogService {
    constructor(private readonly dialog: MatDialog) { }

    openCreateDialog(): Observable<boolean> {
        const emptyCompany = new CompanyResponse();
        emptyCompany.name = '';
        emptyCompany.exchange = '';
        emptyCompany.ticker = '';
        emptyCompany.isin = '';
        emptyCompany.webSite = '';

        const dialogRef = this.dialog.open(CompanyEditDialogComponent, {
            width: '500px',
            data: { company: emptyCompany, isEdit: false },
        });

        return dialogRef.afterClosed();
    }

    openEditDialog(company: CompanyResponse): Observable<boolean> {
        const dialogRef = this.dialog.open(CompanyEditDialogComponent, {
            width: '500px',
            data: { company: company, isEdit: true },
        });

        return dialogRef.afterClosed();
    }
}