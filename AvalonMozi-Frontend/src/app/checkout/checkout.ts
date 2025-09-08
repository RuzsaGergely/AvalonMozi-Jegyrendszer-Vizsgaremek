import { Component, OnInit } from '@angular/core';
import { Footer } from '../footer/footer';
import { Header } from '../header/header';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { CartHandler } from '../../services/carthandler.service';
import { Router } from '@angular/router';
import { BillingInformationDto, OrderClient, OrderRequestDto } from '../../services/moziHttpClient';

@Component({
  selector: 'app-checkout',
  imports: [
    LocalSharedModule,
    Header,
    Footer
  ],
  templateUrl: './checkout.html',
  styleUrl: './checkout.css'
})
export class Checkout implements OnInit {
  
  constructor(
    public cartHandler: CartHandler,
    public orderClient: OrderClient,
    private router: Router
  ){}

  public orderRequest!: OrderRequestDto;
  public billingInformations: BillingInformationDto[] = []
  public selectedBillingInformation!: BillingInformationDto

  ngOnInit(): void {
    this.cartHandler.initCart()

    console.log(this.cartHandler.getCart)

    if(this.cartHandler.getCart.cartItems == null || this.cartHandler.getCart.cartItems.length == 0) {
      this.router.navigate(['kezdolap']);
    }

    this.orderClient.getUserBillingInformations().subscribe((data) => {
      this.billingInformations = data
      console.log(data)
    })
  }

}
