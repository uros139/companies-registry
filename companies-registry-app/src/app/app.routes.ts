import { Routes } from '@angular/router';
import { CompaniesOverviewComponent } from './features/companies/companies-overview/companies-overview.component';

export const routes: Routes = [
    { path: '**', redirectTo: 'home' }, 
    { path: 'home', component: CompaniesOverviewComponent }
];