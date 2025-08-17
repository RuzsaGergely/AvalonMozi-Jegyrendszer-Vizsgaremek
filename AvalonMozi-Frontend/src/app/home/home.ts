import { Component } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { Header } from '../header/header';
import { Footer } from '../footer/footer';

@Component({
  selector: 'app-home',
  imports: [
    LocalSharedModule,
    Header,
    Footer
  ],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {

}
