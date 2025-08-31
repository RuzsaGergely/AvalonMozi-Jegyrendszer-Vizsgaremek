import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { isPlatformBrowser } from '@angular/common';
import { MovieClient, UserClient, UserDto } from './moziHttpClient';
import { Cart, CartItem } from '../app/common/interfaces';

@Injectable({
  providedIn: 'root',
})
export class CartHandler {
  constructor(
    private movieClient: MovieClient,
    @Inject(PLATFORM_ID) private platformId: Object,
  ) {}

  initCart() {
    let cartData = {
        priceWithoutVat: 0,
        priceWithVat: 0,
        cartItems: []
    } as Cart

    localStorage.setItem("cart", JSON.stringify(cartData))
  }

  updateCart() {

  }

  public get getCart(): Cart {
    let cartDataRaw = localStorage.getItem("cart");
    
    let cartData = {
        priceWithoutVat: 0,
        priceWithVat: 0,
        cartItems: []
    } as Cart

    if(cartDataRaw != null) {
        cartData = JSON.parse(localStorage.getItem(cartDataRaw)!);
    } else {
        this.initCart();
    }

    return cartData;
  }
}