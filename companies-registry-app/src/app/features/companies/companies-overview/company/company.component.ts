import { Component, Input } from '@angular/core';
import { CompanyResponse } from '../../../../api/api-reference';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-company',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './company.component.html',
  styleUrl: './company.component.scss'
})
export class CompanyComponent {
  @Input() company!: CompanyResponse;
}
