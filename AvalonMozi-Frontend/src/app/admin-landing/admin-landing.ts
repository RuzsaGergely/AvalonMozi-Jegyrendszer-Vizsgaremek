import { Component, OnInit } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-admin-landing',
  imports: [
    LocalSharedModule
  ],
  templateUrl: './admin-landing.html',
  styleUrl: './admin-landing.css'
})
export class AdminLanding implements OnInit {
  constructor(
    private userAuth: AuthService,
    private router: Router
  ){}

  ngOnInit(): void {
    if(!this.userAuth.isAdminOrEmployee) {
      this.router.navigate(['kezdolap']).then(() => {
        window.location.reload();
      });
    }
  }

}
