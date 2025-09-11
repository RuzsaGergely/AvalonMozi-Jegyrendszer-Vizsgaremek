import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  imports: [],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.css'
})
export class AdminDashboard implements OnInit {

  constructor(
    private userAuth: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    if (!this.userAuth.isAdminOrEmployee) {
      this.router.navigate(['kezdolap']).then(() => {
        window.location.reload();
      });
    }
  }

}
