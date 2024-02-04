import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ApiService } from '../../../../api.service.spec';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  userID: number = 0;
  cartDatas: any;
  cartID: any;
  constructor(private http: HttpClient, private apiService: ApiService, private cdr: ChangeDetectorRef, private router: Router, private route: ActivatedRoute, private toastr: ToastrService) { }

  ngOnInit() {

    this.route.params.subscribe((params: any) => {
      this.userID = params['userID'];
      this.fetchData();
    });
  }

  fetchData() {
    this.apiService.getCartDetail(this.userID).subscribe((response: any) => {
      if (response.success) {
        this.cartDatas = response.data;
        this.cartID = response.data.cardId;
        console.log(this.cartDatas)
      }
      else if (!response.success && response.isUser) {
        console.log(response.message)
      }
      else if (response.isUser) {
        console.log(response.message)
      }
      else {
        console.log(response.message)
      }
    })
  }

  calculateTotalPrice(): number {
    let total = 0;
    this.cartDatas.forEach((item : any) => {
      total += item.totalPrice;
    });
    return total;
  }
  deleteOrder(productID: any) {
    let dataToSend = {
      userID: this.userID,
      productID: productID,

    }
    this.apiService.removeItem(dataToSend).subscribe(response => {
      if (response.success) {
        this.fetchData();
        this.cdr.detectChanges();
      }
    });
  }

  payment() {
    this.router.navigate(['payment', this.userID]);
  }

  deleteCart() {
    this.apiService.removeCart(this.userID).subscribe((response : any )=> {
      if (response.success) {
        this.fetchData();
        this.cdr.detectChanges();
      }
    })
     

  }
}
