import { NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { CompanyComponent } from './company/company.component';
import { Client, CompanyResponse } from '../../../api/api-reference';
import { CompanyDialogService } from '../company-edit-dialog/services/company-dialog.service';
import { CompanySearchService } from '../../../shared/services/company-search.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-companies-overview',
  standalone: true,
  imports: [NgFor, CompanyComponent],
  templateUrl: './companies-overview.component.html',
  styleUrls: ['./companies-overview.component.scss'],
})
export class CompaniesOverviewComponent implements OnInit {
  companies: CompanyResponse[] = [];
  private destroy$ = new Subject<void>();

  constructor(
    private client: Client,
    private companyDialogService: CompanyDialogService,
    private searchService: CompanySearchService
  ) {}

  ngOnInit(): void {
    this.loadCompanies();

    this.searchService.isin$
      .pipe(takeUntil(this.destroy$))
      .subscribe((isin) => {
        this.loadCompanies(isin);
      });
  }

  loadCompanies(isin?: string | null) {
    this.client
      .companiesAll(isin ?? '')
      .subscribe({
        next: (res) => (this.companies = res),
        error: (err) => console.error('Error fetching companies:', err),
      });
  }

  onCreateCompany(): void {
    this.companyDialogService.openCreateDialog().subscribe((result) => {
      if (result) {
        this.loadCompanies();
      }
    });
  }

  onEditCompany(company: CompanyResponse): void {
    this.companyDialogService.openEditDialog(company).subscribe((result) => {
      if (result) {
        this.loadCompanies();
      }
    });
  }
}