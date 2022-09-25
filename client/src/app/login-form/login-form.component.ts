import { Component } from '@angular/core';
import {FormControl, FormGroup} from '@angular/forms';
import {Router} from "@angular/router";
import { HttpClient } from '@angular/common/http';
import {Buffer} from 'buffer';
import { ApiService } from '../api.service';

export interface Customer {
  "CustomerID": number,
  "Access-Token": string
}

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})

export class LoginFormComponent {

  constructor(private router: Router, private http: HttpClient, private service: ApiService) { 
    this.loginResponse = '';
  }

  // moves label when an input is focused, purely cosmetic
  usernameLabelShift = false;
  passwordLabelShift = false;

  shiftLabel(label: string){
    if (label === "username") {
      this.usernameLabelShift = true;
    } else if (label === "password") {
      this.passwordLabelShift = true;
    }
  }

  // login credentials to be passed to auth API
  loginData = new FormGroup({
      username : new FormControl(''),
      password : new FormControl('')
  })
  
  loginFailed = false;
  loginResponse: any;

  authenticateUser() {
    let credentialBase: string = `${this.loginData.value.username}:${this.loginData.value.password}`;

    let encodedString = Buffer.from(credentialBase).toString("base64");
    let authCredentials = `Basic ${encodedString}`;

    this.sendLoginRequest(authCredentials);
  }

  loginBtnText: string = 'Sign in';

  sendLoginRequest(authCredentials: string): void {
    this.loginBtnText = 'Loading...';

    this.service.postLogin(authCredentials)
      .subscribe((result: any) => {
        this.loginResponse = result
        
        // if body.id != 0, login successful
        // parse response & determine next step
        if (this.loginResponse.status === 200) {
          // response body should return customer info
          const customer: Customer = this.loginResponse.body;
          // save token to localStorage
          localStorage.setItem('customer', JSON.stringify(customer));
          // re-route user to productPage
          this.router.navigate(["/productPage"]);

        } else {
          // let the user know that the login failed
          console.log(`Login failed`)
          this.loginFailed = true;
          this.loginBtnText = 'Sign in';
        }
      })
  }
}
