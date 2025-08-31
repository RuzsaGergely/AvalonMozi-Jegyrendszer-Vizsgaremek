import { Component, OnInit } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { Header } from '../header/header';
import { Footer } from '../footer/footer';
import { CartHandler } from '../../services/carthandler.service';
import { Router } from '@angular/router';

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
export class Cart implements OnInit {
  
  constructor(
    public cartHandler: CartHandler,
    private router: Router
  ){}

  ngOnInit(): void {
    this.cartHandler.initCart()

    console.log(this.cartHandler.getCart)
  }

  removeFromCart(uid: number) {
    console.log("Removing cart item.")

    this.cartHandler.removeCartItem(uid)

    this.router.navigate(['kosar']);
  }

}
