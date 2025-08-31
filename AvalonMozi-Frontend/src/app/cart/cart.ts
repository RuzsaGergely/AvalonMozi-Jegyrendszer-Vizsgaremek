import { Component } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { Header } from '../header/header';
import { Footer } from '../footer/footer';

@Component({
  selector: 'app-cart',
  imports: [
    LocalSharedModule,
    Header,
    Footer
  ],
  templateUrl: './cart.html',
  styleUrl: './cart.css'
})
export class Cart {

}
