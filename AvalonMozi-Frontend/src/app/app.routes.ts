import { Routes } from '@angular/router';
import { Login } from './login/login';
import { Home } from './home/home';
import { Register } from './register/register';
import { Contact } from './contact/contact';
import { Feedback } from './feedback/feedback';
import { Movies } from './movies/movies';
import { MovieById } from './movie-by-id/movie-by-id';

export const routes: Routes = [
    { path: 'bejelentkezes', component: Login },
    { path: 'kezdolap', component: Home },
    { path: 'regisztracio', component: Register },
    { path: 'kapcsolat', component: Contact },
    { path: 'visszajelzes', component: Feedback },
    { path: 'filmek', component: Movies },
    { path: 'film/:technicalid', component: MovieById },
    { path: '*', component: Home },
    { path: '', component: Home }
];
