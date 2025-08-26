import { Component, OnInit } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { Header } from '../header/header';
import { Footer } from '../footer/footer';

@Component({
  selector: 'app-contact',
  imports: [
    LocalSharedModule,
    Header,
    Footer
  ],
  templateUrl: './contact.html',
  styleUrl: './contact.css'
})
export class Contact implements OnInit {

  ngOnInit(): void {
  }

}
