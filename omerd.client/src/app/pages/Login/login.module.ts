import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {loginComponent } from './login.component'




@NgModule({
  declarations: [
    loginComponent
  ],
  exports: [
    loginComponent
  ],

  imports: [
    BrowserModule,
    HttpClientModule,
    CommonModule,
    FormsModule
  ],
  providers: [],
  bootstrap: []
})
export class loginModule { }
