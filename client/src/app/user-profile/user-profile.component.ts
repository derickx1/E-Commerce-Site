import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Router} from "@angular/router"
import {Customer} from "../login-form/login-form.component";
import { ApiService } from '../api.service';

export interface ICustomer {
  id: number,
  name: string,
  shipping_address: string
}

export interface Order {
  order_date: string
}

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})

export class UserProfileComponent implements OnInit {

  constructor(private router: Router, private http: HttpClient, private service: ApiService) { }

  ngOnInit(): void {
    this.setUserProfile();
  }

  @Input() openProfileModalCommand: boolean = false;
  ngOnChanges(changes: SimpleChanges) {
    // listens for changes on product page to see if the user profile icon has been clicked
    try {
      if (changes["openProfileModalCommand"].previousValue != changes["openProfileModalCommand"].currentValue) {
        this.toggleProfileModal();
      }
    } catch(ex: any) {

    }
  } 

  @Input() customer: Customer = {
    "CustomerID": 0,
    "Access-Token": ''
  };

  // sets the name displayed at the top of the user profile modal
  userProfile: string = ''
  setUserProfile() {
    this.service.getUserProfile(this.customer["CustomerID"], this.customer["Access-Token"])
      .subscribe((result) => {
        const resultBody: any = result.body;
        this.userProfile = resultBody.name;
      })
  }

  // display the user's profile modal
  profileModalToggle = true;
  toggleProfileModal() {
    this.profileModalToggle = !this.profileModalToggle;
  }

  // display orders
  displayViewOrder: boolean = false;
  toggleViewOrderDisplay() {
    this.displayViewOrder = !this.displayViewOrder
  }

  orderHistory: any;
  viewOrderHistory() {
    this.service.getOrderHistory(this.customer["CustomerID"], this.customer["Access-Token"])
      .subscribe((result) => {
        this.orderHistory = result.body
      })
  }

  // move user back to login page & remove access token
  logout() {
    localStorage.clear();
    this.router.navigate(["/"]);
  }

}
