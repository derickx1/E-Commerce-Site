import { Component } from '@angular/core';
import {ActivatedRoute, Router, Params} from "@angular/router";
import { Product } from '../product-page/product-page.component';
import { Customer } from "../login-form/login-form.component";
import { HttpClient } from '@angular/common/http';

interface Session {
  sessionUrl: string;
}

@Component({
  selector: 'app-checkout-page',
  templateUrl: './checkout-page.component.html',
  styleUrls: ['./checkout-page.component.css']
})

export class CheckoutPageComponent {

  constructor(private router: Router, private route: ActivatedRoute, private http: HttpClient) {
    // if user does not have login token, re-route them to login page
    const checkTokenPresent: Customer = JSON.parse(localStorage.getItem("customer") || '{}');
    if (!checkTokenPresent['Access-Token']) {
      this.router.navigate(["/"]);
    }

    this.route.queryParams.subscribe((params) => {
      console.log(params["cart"]);
      let filledCart: Product[] = JSON.parse(params["cart"]);
      this.cart = filledCart;
      this.cartPriceTotal = this.cartTotal();
    });
  }

  customer: Customer = {
    "CustomerID": 0,
    "Access-Token": ''
  };

  cart: Product[] = [];
  cartPriceTotal = 0;

  cartTotal() {
    let cartPriceTotal = 0;
    this.cart.forEach(cartItem => cartPriceTotal += cartItem.price)
    return cartPriceTotal;
  }

  // send request to Stripe API
  // set up confirmation modal
  confirmationModal = false;
  makePurchase() {
    const customer: Customer = JSON.parse(localStorage.getItem("customer") || '{}');
    this.customer = customer;
    // make request to payment api
    this.http.post<Session>("https://team7project2api.azurewebsites.net/orders/checkout/stripe", JSON.stringify(this.cart), {
      headers: {
        "Authorization": `Bearer ${customer["Access-Token"]}`,
        "Content-Type": "application/json"
      },
      responseType: "json"
    }).subscribe((session: Session) => {
      window.open(session.sessionUrl, "_blank")
    })
    this.confirmationModal = !this.confirmationModal;
  }

  // re-route user back to product page, but maintain login
  cancelOrder() {
    // will need to pass the current cart so that it is not lost
    this.router.navigate(["/productPage"], {queryParams : {cart: JSON.stringify(this.cart)}});
  }

  // return user back to product page with an empty cart
  continueShopping() {
    // cart is automatically reset
    // return user to productPage
    this.router.navigate(["/productPage"]);
  }

}
