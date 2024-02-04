import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../../api.service.spec';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-main',
  templateUrl: './mainPage.component.html',
  styleUrls: ['./mainPage.component.scss']
})
export class MainPageComponent implements OnInit {
  bestProductList: any;
  latestProducts: any;
  constructor(private http: HttpClient, private apiService: ApiService, private route: ActivatedRoute) { }

  ngOnInit() {
    const userID = this.route.snapshot.queryParams['userID'];

    this.getBestProduct();
    this.getLatestProduct();
  }

  getBestProduct() {
    this.apiService.getBestProducts().subscribe((response : any)=> {
      if (response.success) {
        this.bestProductList = response.data;
      }
      else {

      }
    })
  }
  getLatestProduct() {
    this.apiService.getLatestProducts().subscribe((response: any) => {
      this.latestProducts = response.data;
      console.log(this.latestProducts)

    })
  }
}
