import { Component, OnInit } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { Header } from '../header/header';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-movie-by-id',
  imports: [
    LocalSharedModule,
    Header
  ],
  templateUrl: './movie-by-id.html',
  styleUrl: './movie-by-id.css'
})
export class MovieById implements OnInit {

  constructor(
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const technicalid = this.route.snapshot.paramMap.get('technicalid');
    console.log('Technical ID:', technicalid);
  }

}
