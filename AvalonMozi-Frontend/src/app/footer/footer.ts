import { Component } from '@angular/core';
import { LocalSharedModule } from '../localshared/local-shared-module';

@Component({
  selector: 'app-footer',
  imports: [
    LocalSharedModule
  ],
  templateUrl: './footer.html',
  styleUrl: './footer.css'
})
export class Footer {

}
