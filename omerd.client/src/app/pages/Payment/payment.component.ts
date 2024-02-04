import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../../api.service.spec';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss']
})
export class PaymentComponent implements OnInit {
  kartName: any;
  KartNo: any;
  expirationDate: any;
  CVV: any;
  userID: any
  constructor(private http: HttpClient, private apiService: ApiService, private route: ActivatedRoute,private router: Router) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.userID = params['userID'];
    });  }
  submitOrder() {
    const dataToSent = {
      KartName: this.kartName,
      Expiration: this.expirationDate,
      cvv: this.CVV,
      userID: this.userID,
      cardNo: this.KartNo
    }

    this.apiService.submitCart(dataToSent).subscribe((response: any) => {
      if (response.success) {
        this.router.navigate(['anasayfa', this.userID]);
      }
    });



  }

}
