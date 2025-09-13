import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { Footer } from '../footer/footer';
import { MessageService } from 'primeng/api';
import { MovieClient, MovieDateDto, MovieDto } from '../../services/moziHttpClient';

@Component({
  selector: 'app-admin-dashboard',
  imports: [
    LocalSharedModule,
    Footer
  ],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.css',
  providers: [
    MessageService
  ]
})
export class AdminDashboard implements OnInit {

  constructor(
    private userAuth: AuthService,
    private router: Router,
    private messageService: MessageService,
    private movieClient: MovieClient
  ) { }

  public movieList: MovieDto[] = []

  public selectedMovie: MovieDto = {
    ageRestriction: "",
    coverImageBase64: "",
    dates: [] as MovieDateDto[],
    description: "",
    seoFriendlyTitle: "",
    technicalId: "",
    ticketPrice: 0,
    title: ""
  } as MovieDto
  
  public showSelectedMovie: boolean = false;

  public newMovie: MovieDto = {
    ageRestriction: "",
    coverImageBase64: "",
    dates: [] as MovieDateDto[],
    description: "",
    seoFriendlyTitle: "",
    technicalId: "",
    ticketPrice: 0,
    title: ""
  } as MovieDto
  public showNewMovie: boolean = false;

  ngOnInit(): void {
    if (!this.userAuth.isAdminOrEmployee) {
      this.router.navigate(['kezdolap']).then(() => {
        window.location.reload();
      });
    }

    this.movieClient.getMovies().subscribe((data) => {
      this.movieList = data
    })
  }

  openSelectedMovie(technicalId: string) {
    this.movieList.forEach(x => {
      if (x.technicalId == technicalId) {
        this.selectedMovie = x;
        this.showSelectedMovie = true;
      }
    })
  }

}
