import { Component, OnInit } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { Header } from '../header/header';
import { ActivatedRoute, Router } from '@angular/router';
import { Footer } from "../footer/footer";
import { MovieClient, MovieDateDto, MovieDto } from '../../services/moziHttpClient';
import { CartHandler } from '../../services/carthandler.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-movie-by-seotitle',
  imports: [
    LocalSharedModule,
    Header,
    Footer
  ],
  providers: [MessageService],
  templateUrl: './movie-by-seotitle.html',
  styleUrl: './movie-by-seotitle.css'
})
export class MovieBySeoTitle implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private movieClient: MovieClient,
    private cartHandler: CartHandler,
    private messageService: MessageService,
    private router: Router
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

  addToCart(qtyAdd: number, movie: MovieDto, movieDate: MovieDateDto) {
    
    this.cartHandler.addToCart({
      qty: qtyAdd,
      dateReadable: `${movieDate.dateFrom.toISOString()} - ${movieDate.dateTo.toISOString()}`,
      dateTechnicalId: movieDate.technicalId!,
      movieReadable: movie.title,
      movieTechnicalId: movie.technicalId!,
      uid: Date.now(),
      ticketprice: movie.ticketPrice
    })

    this.messageService.add({
      severity: 'success',
      summary: 'Siker',
      detail: 'A terméket a kosárba helyeztük! Átirányítás...',
    });

    setTimeout(() => {
      console.log('Wait for 1 seconds...');
      this.router.navigate(['kosar']);
    }, 1000);
  }

}
