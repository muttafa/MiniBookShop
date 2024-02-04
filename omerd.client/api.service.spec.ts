import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient) { }

  baseUrl = `https://localhost:7226`;




  getProducts(): Observable<any> {
    const url = `${this.baseUrl}/Products/getProducts`;
    return this.http.get(url).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(error.message);
        return throwError('Something went wrong; please try again later.');
      })
    );
  }

  getBestProducts(): Observable<any> {
    const url = `${this.baseUrl}/Products/getBestProducts`;
    return this.http.get(url).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(error.message);
        return throwError('Something went wrong; please try again later.');
      })
    );
  }
  getLatestProducts(): Observable<any> {
   const url = `${this.baseUrl}/Products/getLatestProducts`;
    return this.http.get(url).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(error.message);
        return throwError('Something went wrong; please try again later.');
      })
    );
  }
  filterProducts(filterValue: any): Observable<any> {
    const url = `${this.baseUrl}/Products/filterProducts`;
    return this.http.post(url, filterValue).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(error.message);
        return throwError('Something went wrong; please try again later.');
      })
    );
  }
  addCart(productID: any): Observable<any> {
    const url = `${this.baseUrl}/Cart/addCart`;
    return this.http.post(url, productID).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(error.message);
        return throwError('Something went wrong; please try again later.');
      })
    );
  }
  logIn(loginData: any): Observable<any> {
    const url = `${this.baseUrl}/User/login`;
    return this.http.post(url, loginData).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(error.message);
        return throwError('Something went wrong; please try again later.');
      })
    );
  }
  getCartDetail(userID: number): Observable<any> {
    const url = `${this.baseUrl}/Cart/getCartDetail/${userID}`;
    return this.http.get(url).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(error.message);
        return throwError('Something went wrong; please try again later.');
      })
    );
  }
  removeItem(orderDetail: any): Observable<any> {
    const url = `${this.baseUrl}/Cart/removeItem`;
    return this.http.post(url, orderDetail).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(error.message);
        return throwError('Something went wrong; please try again later.');
      })
    );
  }
  submitCart(payment: any): Observable<any> {
    const url = `${this.baseUrl}/Cart/submitPayment`;
    return this.http.post(url, payment).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(error.message);
        return throwError('Something went wrong; please try again later.');
      })
    );
  }
  removeCart(userID: number): Observable<any> {
    const url = `${this.baseUrl}/Cart/deleteCart/${userID}`;
    return this.http.get(url).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(error.message);
        return throwError('Something went wrong; please try again later.');
      })
    );
  }

  createUser(newUserData: any): Observable<any> {
    const url = `${this.baseUrl}/User/createUser`;
    return this.http.post(url, newUserData).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error(error.message);
        return throwError('Something went wrong; please try again later.');
      })
    );
  }
  
}
