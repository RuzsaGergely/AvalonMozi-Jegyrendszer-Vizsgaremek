import { Component } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';

@Component({
  selector: 'app-admin-landing',
  imports: [
    LocalSharedModule
  ],
  templateUrl: './admin-landing.html',
  styleUrl: './admin-landing.css'
})
export class AdminLanding {

}
