import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../../api.service.spec';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class loginComponent implements OnInit {
  userName: any
  password: any
  userNameSignIn: any
  passwordSignIn: any
  password2: any
  email: any
  adress: any
  userID: any;
  constructor(private http: HttpClient, private apiService: ApiService,private router: Router,) { }

  ngOnInit() {

  }

  logIn() {
    const dataToSend = {
      userName: this.userName,
      password: this.password
    }
    this.apiService.logIn(dataToSend).subscribe(response => {
      if (response.success) {
        this.router.navigate(['anasayfa', response.userID]);
      }
      else {
      }
    })
  }

  register() {

    if (this.password2 === this.passwordSignIn) {
      const dataToSend = {
        userName: this.userNameSignIn,
        password: this.passwordSignIn,
        email: this.email,
        adress: this.adress
      }

      this.apiService.createUser(dataToSend).subscribe((response: any) => {
        if (response.success) {
          this.router.navigate(['anasayfa', response.userID]);
        }
        else {
          alert(response.message);
        }
      });
    }


  }

}
