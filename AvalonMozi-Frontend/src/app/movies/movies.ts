import { Component, OnInit } from '@angular/core';
import { Header } from '../header/header';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { MovieClient, MovieDto } from '../../services/moziHttpClient';

@Component({
  selector: 'app-movies',
  imports: [
    LocalSharedModule,
    Header
  ],
  templateUrl: './movies.html',
  styleUrl: './movies.css'
})
export class Movies implements OnInit{

  constructor(
    private movieClient: MovieClient
  ) {}

  public moviesFromServer: MovieDto[] = [];

  ngOnInit(): void {
    var request = this.movieClient.getMovies()
    .subscribe((data) => {
      console.log(data)
      this.moviesFromServer = data;
    });
  }

}
