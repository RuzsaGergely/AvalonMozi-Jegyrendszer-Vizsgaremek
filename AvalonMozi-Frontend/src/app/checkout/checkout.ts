import { Component, OnInit } from '@angular/core';
import { Footer } from '../footer/footer';
import { Header } from '../header/header';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { CartHandler } from '../../services/carthandler.service';
import { Router } from '@angular/router';
import { BillingInformationDto, OrderClient, OrderItemRequestDto, OrderRequestDto } from '../../services/moziHttpClient';

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
  ) { }

  public orderRequest!: OrderRequestDto;
  public billingInformations: BillingInformationDto[] = []
  public selectedBillingInformation!: BillingInformationDto

  public showForm: boolean = true;
  public showSuccess: boolean = false;
  public showFailed: boolean = false;

  ngOnInit(): void {
    this.cartHandler.initCart()

    console.log(this.cartHandler.getCart)

    if (this.cartHandler.getCart.cartItems == null || this.cartHandler.getCart.cartItems.length == 0) {
      this.router.navigate(['kezdolap']);
    }

    this.orderClient.getUserBillingInformations().subscribe((data) => {
      this.billingInformations = data
      this.billingInformations.push({
        address1: "",
        address2: "",
        city: "",
        companyName: "",
        county: "",
        name: "",
        technicalId: "NEWBILLINGINFO",
        vatNumber: "",
        zipCode: ""
      } as BillingInformationDto)

      this.selectedBillingInformation = this.billingInformations[this.billingInformations.length - 1]
      console.log(data)
    })
  }

  createOrderRequest(): void {
    this.orderRequest = {
      billingInfo: this.selectedBillingInformation,
      userTechnicalId: "",
      items: [] as OrderItemRequestDto[]
    } as OrderRequestDto

    let items = this.cartHandler.getCart

    items.cartItems.forEach(x => {
      for (let index = 0; index < x.qty; index++) {
        this.orderRequest.items.push({
          dateTimeTechnicalId: x.dateTechnicalId,
          movieTechnicalId: x.movieTechnicalId
        } as OrderItemRequestDto)
      }
    })

    this.orderClient.addNewOrder(this.orderRequest).subscribe(x => {
      this.showForm = false
      if (x.status == 200) {
        this.showSuccess = true;
        localStorage.removeItem('cart')
      } else {
        this.showFailed = true;
      }
    })
  }

}
