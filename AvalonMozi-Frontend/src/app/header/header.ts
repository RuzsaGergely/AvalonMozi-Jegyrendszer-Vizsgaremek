import { Component, OnInit } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-header',
  imports: [
    LocalSharedModule
  ],
  templateUrl: './header.html',
  styleUrl: './header.css'
})
export class Header implements OnInit {
  items: MenuItem[] | undefined;

    ngOnInit() {
        this.items = [
            {
              label: 'Kezdőlap',
              icon: 'pi pi-home',
              routerLink: ['/kezdolap']
            },
            {
              label: 'Filmek',
              icon: 'pi pi-ticket',
              routerLink: ['/filmek']
            },
            {
              label: 'Visszajelzés',
              icon: 'pi pi-comment',
              routerLink: ['/visszajelzes']
            },
            {
              label: 'Kapcsolat',
              icon: 'pi pi-map-marker',
              routerLink: ['/kapcsolat']
            }
        ]
    }
}
