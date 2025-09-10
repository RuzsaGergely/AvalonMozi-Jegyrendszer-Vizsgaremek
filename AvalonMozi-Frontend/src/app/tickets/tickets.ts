import { Component, OnInit } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { Header } from '../header/header';
import { Footer } from '../footer/footer';
import { TicketClient, UserTicketDto } from '../../services/moziHttpClient';
import { QrCodeComponent } from 'ng-qrcode';

@Component({
  selector: 'app-tickets',
  imports: [
    LocalSharedModule,
    Header,
    Footer,
    QrCodeComponent
  ],
  templateUrl: './tickets.html',
  styleUrl: './tickets.css'
})
export class Tickets implements OnInit {

  constructor(
    private ticketClient: TicketClient
  ){}

  public ticketList: UserTicketDto[] = []

  ngOnInit(): void {
    this.ticketClient.getUserTickets().subscribe(x=> {
      this.ticketList = x
    })
  }

}
