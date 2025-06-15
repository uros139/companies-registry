import { NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { CompanyComponent } from './company/company.component';
import { Client, CompanyResponse } from '../../../api/api-reference';
import { CompanyDialogService } from '../company-edit-dialog/services/company-dialog.service';

@Component({
  selector: 'app-companies-overview',
  standalone: true,
  imports: [NgFor, CompanyComponent],
  templateUrl: './companies-overview.component.html',
  styleUrls: ['./companies-overview.component.scss'],
})
export class CompaniesOverviewComponent implements OnInit {
  companies: CompanyResponse[] = [];
  
  constructor(
    private client: Client,
    private companyDialogService: CompanyDialogService
  ) {}

  ngOnInit(): void {
    this.loadCompanies();
  }

  loadCompanies(): void {
    this.client.companiesAll().subscribe((companies) => {
      this.companies = companies;
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