import { Component } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { Header } from '../header/header';

@Component({
  selector: 'app-home',
  imports: [
    LocalSharedModule,
    Header
  ],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {

}
