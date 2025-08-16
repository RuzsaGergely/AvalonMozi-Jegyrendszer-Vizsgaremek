import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenubarModule } from 'primeng/menubar';
import { SharedModule } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { RouterModule } from '@angular/router';
import { CardModule } from 'primeng/card';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedModule,
    MenubarModule,
    ButtonModule,
    RouterModule,
    CardModule
  ],
  exports: [
    CommonModule,
    SharedModule,
    MenubarModule,
    ButtonModule,
    RouterModule,
    CardModule
  ]
})
export class LocalSharedModule { }
