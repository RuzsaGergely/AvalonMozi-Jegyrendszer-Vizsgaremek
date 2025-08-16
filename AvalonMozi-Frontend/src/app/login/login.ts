import { Component } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';

@Component({
  selector: 'app-login',
  imports: [
    LocalSharedModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {

}
