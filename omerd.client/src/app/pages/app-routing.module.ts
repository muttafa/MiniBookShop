import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from './MainPage/mainPage.component';
import { ProductsComponent } from './ProductsPage/products.component';
import { loginComponent } from './Login/login.component'; 
import { CartComponent } from './Cart/cart.component';
import { PaymentComponent } from './Payment/payment.component';

const routes: Routes = [
  { path: 'login', component: loginComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' }, 
  { path: 'anasayfa/:userID', component: MainPageComponent },
  { path: 'products/:userID', component: ProductsComponent },
  { path: 'cart/:userID', component: CartComponent },
  { path: 'payment/:userID', component: PaymentComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
