import { Component, OnInit } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { Header } from '../header/header';
import { ActivatedRoute } from '@angular/router';
import { Footer } from "../footer/footer";
import { MovieClient, MovieDto } from '../../services/moziHttpClient';

@Component({
  selector: 'app-movie-by-seotitle',
  imports: [
    LocalSharedModule,
    Header,
    Footer
],
  templateUrl: './movie-by-seotitle.html',
  styleUrl: './movie-by-seotitle.css'
})
export class MovieBySeoTitle implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private movieClient: MovieClient
  ) {}

  public movie?: MovieDto;
  public selectedItems: any = {};

  ngOnInit(): void {
    const seotitle = this.route.snapshot.paramMap.get('seotitle') ?? "";
    console.log('Seo Friendly Title:', seotitle);

    this.movieClient.getMovieBySeoTitle(seotitle).subscribe((data) => {
      this.movie = data;

      data.dates.forEach((date) => {
        this.selectedItems[date.technicalId!] = {
          qty: 0
        }
      })

      console.log(this.selectedItems)
    })
  }

}
