import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { isPlatformBrowser } from '@angular/common';
import { MovieClient, MovieDto, UserClient, UserDto } from './moziHttpClient';
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
    if(localStorage.getItem('cart') == null) {
        let cartData = {
            priceWithoutVat: 0,
            priceWithVat: 0,
            cartItems: [] as CartItem[]
        } as Cart
        localStorage.setItem("cart", btoa(JSON.stringify(cartData)))
    }
  }

  removeCartItem(uid: number) {
    let currentCart = this.getCart

    const index = currentCart.cartItems.findIndex(element => element.uid === uid);

    if (index !== -1) {
        currentCart.cartItems.splice(index, 1);
    }

    currentCart.priceWithVat = 0
    currentCart.priceWithoutVat = 0
    
    currentCart.cartItems.forEach(x=> {
        currentCart.priceWithoutVat += x.ticketprice
    })

    currentCart.priceWithVat = Math.round(currentCart.priceWithoutVat * 1.18)

    console.log(JSON.stringify(currentCart))

    localStorage.setItem("cart", btoa(JSON.stringify(currentCart)))    
  }

  addToCart(cartItem: CartItem) {
    let currentCart = this.getCart

    currentCart.cartItems.push(cartItem)

    currentCart.priceWithVat = 0
    currentCart.priceWithoutVat = 0
    currentCart.cartItems.forEach(x=> {
        currentCart.priceWithoutVat += cartItem.ticketprice
    })

    currentCart.priceWithVat = Math.round(currentCart.priceWithoutVat * 1.18)

    console.log(JSON.stringify(currentCart))

    localStorage.setItem("cart", btoa(JSON.stringify(currentCart)))
  }

  public get getCart(): Cart {
    let cartDataRaw = localStorage.getItem("cart");

    let cartData = {
        priceWithoutVat: 0,
        priceWithVat: 0,
        cartItems: []
    } as Cart

    if(cartDataRaw != null) {
        cartData = JSON.parse(atob(cartDataRaw!));
    } else {
        this.initCart();
    }

    return cartData;
  }
}