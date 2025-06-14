import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { CompanyResponse } from '../../../../api/api-reference';
import { CompanyEditDialogComponent } from '../company-edit-dialog.component';


@Injectable({ providedIn: 'root' })
export class CompanyDialogService {
    constructor(private readonly dialog: MatDialog) { }

    openCreateDialog(): Observable<CompanyResponse | undefined> {
        const emptyCompany = new CompanyResponse();
        emptyCompany.name = '';
        emptyCompany.exchange = '';
        emptyCompany.ticker = '';
        emptyCompany.isin = '';
        emptyCompany.webSite = '';

        const dialogRef = this.dialog.open(CompanyEditDialogComponent, {
            width: '500px',
            data: emptyCompany,
        });

        return dialogRef.afterClosed();
    }

    openEditDialog(company: CompanyResponse): Observable<CompanyResponse | undefined> {
        const dialogRef = this.dialog.open(CompanyEditDialogComponent, {
            width: '500px',
            data: company,
        });

        return dialogRef.afterClosed();
    }
}
