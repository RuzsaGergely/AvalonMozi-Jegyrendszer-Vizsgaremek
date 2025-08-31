import { Routes } from '@angular/router';
import { Login } from './login/login';
import { Home } from './home/home';
import { Register } from './register/register';
import { Contact } from './contact/contact';
import { Feedback } from './feedback/feedback';
import { Movies } from './movies/movies';
import { MovieBySeoTitle } from './movie-by-seotitle/movie-by-seotitle';
import { Cart } from './cart/cart';
import { AdminDashboard } from './admin-dashboard/admin-dashboard';
import { AdminTicketcheck } from './admin-ticketcheck/admin-ticketcheck';
import { AdminLanding } from './admin-landing/admin-landing';

export const routes: Routes = [
    { path: 'bejelentkezes', component: Login },
    { path: 'kezdolap', component: Home },
    { path: 'regisztracio', component: Register },
    { path: 'kapcsolat', component: Contact },
    { path: 'visszajelzes', component: Feedback },
    { path: 'filmek', component: Movies },
    { path: 'film/:seotitle', component: MovieBySeoTitle },
    { path: 'kosar', component: Cart },
    { path: 'admin/vezerlopult', component: AdminDashboard },
    { path: 'admin/jegyellenorzes', component: AdminTicketcheck },
    { path: 'admin', component: AdminLanding },
    { path: '**', redirectTo: "/kezdolap" },
];
