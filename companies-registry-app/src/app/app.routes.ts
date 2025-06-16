import { Routes } from '@angular/router';
import { CompaniesOverviewComponent } from './features/companies/companies-overview/companies-overview.component';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'home', component: CompaniesOverviewComponent },
  { path: '**', redirectTo: 'login' },
];