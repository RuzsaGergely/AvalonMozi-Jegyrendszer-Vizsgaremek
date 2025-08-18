import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeuix/themes/aura';
import { definePreset } from '@primeuix/themes';

import { routes } from './app.routes';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { provideClientHydration } from '@angular/platform-browser';
import { environment } from '../environments/environment';

import { API_BASE_URL } from '../services/moziHttpClient';
import { JwtInterceptorService } from '../services/jwt-interceptor.service';

const customPreset = definePreset(Aura, {
  semantic: {
      primary: {
          50: '#83c98a',
          100: '#6abe73',
          200: '#51b35b',
          300: '#38a844',
          400: '#1f9d2c',
          500: '#069215',
          600: '#058313',
          700: '#057511',
          800: '#04660f',
          900: '#04580d',
          950: '#03490b'
      }
  }
});

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    providePrimeNG({
        theme: {
          preset: customPreset,
          options: {
            darkModeSelector: '',
            cssLayer: {
              name: 'primeng',
              order: 'theme, base, primeng',
            },
          },
        }
    }),
    provideClientHydration(),
    { 
      provide: API_BASE_URL, useValue: environment.apiBaseUrl },
      provideHttpClient(withInterceptorsFromDi()),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptorService,
      multi: true,
    },
  ]
};
