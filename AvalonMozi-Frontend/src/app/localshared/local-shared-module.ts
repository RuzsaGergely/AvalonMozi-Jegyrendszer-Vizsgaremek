import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenubarModule } from 'primeng/menubar';
import { SharedModule } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { RouterModule } from '@angular/router';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedModule,
    MenubarModule,
    ButtonModule,
    RouterModule,
    CardModule,
    TableModule,
    FormsModule,
    InputNumberModule,
    InputTextModule
  ],
  exports: [
    CommonModule,
    SharedModule,
    MenubarModule,
    ButtonModule,
    RouterModule,
    CardModule,
    TableModule,
    FormsModule,
    InputNumberModule,
    InputTextModule
  ]
})
export class LocalSharedModule { }
