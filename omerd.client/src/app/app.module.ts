import { HttpClientModule } from '@angular/common/http';
import { Injectable, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { ApiService } from '../../api.service.spec'; 
import { NavbarModule } from './pages/NavBar/navbar.module';
import { AppRoutingModule } from './pages/app-routing.module';
import { MainPageModule } from './pages/MainPage/mainPage.module';
import { ProductsModule } from './pages/ProductsPage/products.module';
import { loginModule } from './pages/Login/login.module'
import { CartModule } from './pages/Cart/cart.module'
import { PaymentModule } from './pages/Payment/payment.module'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { FooterModule } from './pages/Footer/footer.module'


@Injectable({
  providedIn: 'root' 
})

  @NgModule({
    declarations: [
      AppComponent
    ],
    imports: [
      MainPageModule,
      BrowserModule,
      HttpClientModule,
      NavbarModule,
      AppRoutingModule,
      CommonModule,
      ProductsModule,
      FormsModule,
      loginModule,
      CartModule,
      PaymentModule,
      BrowserAnimationsModule,
      ToastrModule.forRoot(),
      FooterModule,
    ],
  providers: [ApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
