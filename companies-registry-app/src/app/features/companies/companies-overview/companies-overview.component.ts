import { NgFor } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { CompanyComponent } from './company/company.component';
import { Client, CompanyResponse } from '../../../api/api-reference';

@Component({
  selector: 'app-companies-overview',
  standalone: true,
  imports: [NgFor, CompanyComponent],
  templateUrl: './companies-overview.component.html',
  styleUrl: './companies-overview.component.scss'
})
export class CompaniesOverviewComponent implements OnInit {
  companies: CompanyResponse[] = [];

  constructor(private client: Client) {}

  ngOnInit(): void {
    this.client.companiesAll().subscribe(companies => {
      this.companies = companies;
    });
  }
}