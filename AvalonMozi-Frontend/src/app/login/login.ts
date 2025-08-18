import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [
    LocalSharedModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.css',
  providers: [MessageService]
})


export class Login implements OnInit {

  public username: string = ""
  public password: string = ""

  constructor(
    private authService: AuthService,
    private router: Router,
    private messageService: MessageService,
    @Inject(PLATFORM_ID) private platformId: Object,
  ) {}

  ngOnInit(): void {
    
  }

  login() {
    this.authService
      .login(this.username, this.password)
      .subscribe((response) => {
        if (isPlatformBrowser(this.platformId)) {
          if (response != "ERROR_INVALID_USERNAME_OR_PASSWORD") {
            localStorage.removeItem('token');
            localStorage.setItem('token', response);

            this.messageService.clear();
            this.router.navigate(['kezdolap']);
          } else {
            this.messageService.add({
              severity: 'warn',
              summary: 'Bejelentkezési hiba',
              detail: 'Hibás email cím vagy jelszó!',
            });
          }
        }
      });
  }
}
