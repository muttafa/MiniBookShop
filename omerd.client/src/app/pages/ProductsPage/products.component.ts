import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ApiService } from '../../../../api.service.spec';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  filterProductName: any = null
  filterPrice: any = null;
  stockQuantity: any = null;
  star: any = null;
  createDate: any = null;
  allProducts: any = null;
  originalData: any = null;
  userID: any = null
  constructor(private http: HttpClient,private apiService: ApiService,private cdr: ChangeDetectorRef,private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.userID = params['userID'];
      console.log('UserID:', this.userID);
    });

    this.getAllProducts();
  }

  getAllProducts() {
    this.apiService.getProducts().subscribe((response => {
      this.allProducts = response.data;
      console.log(this.allProducts)
      this.originalData = response.data;
    }))
  }

  filterProducts() {

    const dataToSend = {
      productName: this.filterProductName,
      price: this.filterPrice,
      stockQuantity: this.stockQuantity,
      star: this.star,
      createDate: this.createDate,
    }

    this.apiService.filterProducts(dataToSend).subscribe((response: any) => {
        this.allProducts = response.data;
        this.cdr.detectChanges();
      });
    }

  addToCart(productID: any) {
    let dataToSend = {
      UserID: this.userID,
      ProductID: productID,
    }
    this.apiService.addCart(dataToSend).subscribe((response: any) => { })
  }



  }




