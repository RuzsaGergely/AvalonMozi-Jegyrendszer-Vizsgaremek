import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { isPlatformBrowser } from '@angular/common';
import { UserClient, UserDto } from './moziHttpClient';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private http: HttpClient,
    private router: Router,
    private userClient: UserClient,
    @Inject(PLATFORM_ID) private platformId: Object,
  ) {}

  login(username: string, password: string): Observable<any> {
    return this.userClient.login(username, password);
  }

  logout(): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem('token');
      this.router.navigate(['kezdolap']).then(() => {
            window.location.reload();
        });
    }
  }

  public get token(): string | null {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem('token');
    } else {
      return null;
    }
  }

  public get isLoggedIn(): boolean {
    return this.token !== null;
  }

  public get isAdminOrEmployee(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      var userProfileFromStorage = localStorage.getItem('userprofile');
      if(!userProfileFromStorage) {
        return false;
      }
      let userProfile = JSON.parse(atob(userProfileFromStorage) ?? "") as UserDto
      return userProfile.roles.some(x=> x.technicalName == "ADMIN" || x.technicalName == "EMPLOYEE")
    } else {
      return false;
    }
  }
}