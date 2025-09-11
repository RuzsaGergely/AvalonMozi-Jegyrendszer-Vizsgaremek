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
import { PasswordModule } from 'primeng/password';
import { FloatLabelModule } from 'primeng/floatlabel';
import { ToastModule } from 'primeng/toast';
import { ImageModule } from 'primeng/image';
import { TextareaModule } from 'primeng/textarea';
import { SelectModule } from 'primeng/select';
import { RadioButtonModule } from 'primeng/radiobutton';
import { DialogModule } from 'primeng/dialog';



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
    InputTextModule,
    PasswordModule,
    FloatLabelModule,
    ToastModule,
    ImageModule,
    TextareaModule,
    SelectModule,
    RadioButtonModule,
    DialogModule
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
    InputTextModule,
    PasswordModule,
    FloatLabelModule,
    ToastModule,
    ImageModule,
    TextareaModule,
    SelectModule,
    RadioButtonModule,
    DialogModule
  ]
})
export class LocalSharedModule { }
