import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { API_BASE_URL } from './api/api-reference';
import { environment } from '../assets/environments/environment';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(),
    provideRouter(routes),
    { provide: API_BASE_URL, useValue: environment.apiUrl },
    // other providers here
  ]
};
