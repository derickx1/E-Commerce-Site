import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router"
import { HttpClient } from '@angular/common/http';
import {Customer} from "../login-form/login-form.component";
import { ApiService } from '../api.service';

export interface Product {
  id: number,
  name: string,
  price: number,
  material: string,
  type: string,
  imgURL: string,
}

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css']
})

export class ProductPageComponent implements OnInit {

  constructor(private router: Router, private http: HttpClient, private service: ApiService) {
    // if user does not have login token, re-route them to login page
    const checkTokenPresent: Customer = JSON.parse(localStorage.getItem("customer") || '{}');
    if (!checkTokenPresent['Access-Token']) {
      this.router.navigate(["/"]);
    } else {
      this.accessToken = checkTokenPresent['Access-Token']
    }
  }

  ngOnInit(): void {
    this.fetchAllProducts();
  }

  accessToken: string = '';

  customer: Customer = {
    "CustomerID": 0,
    "Access-Token": ''
  };

  allProducts: Array<Product> = [];
  // for infinite scrolling products; used in combination with allProducts
  infiniteProducts: Array<Product> = [];
  productFetchError: boolean = false;

  // for grabbing sets of products for pagination effect
  startRow: number = 0;
  endRow: number = 9;

  fetchAllProducts() {
    // will not parse without {} since getItem can be null
    const customer: Customer = JSON.parse(localStorage.getItem("customer") || '{}');
    this.customer = customer;

    this.service.getProducts(this.startRow, this.endRow, customer["Access-Token"])
      .subscribe((result: any) => {
        if (result.status === 200) {
          const products = result.body;

          products.forEach((item: Product) => {
            this.allProducts.push(item);
          })

          // update the row values
          this.startRow += 9;
          this.endRow += 9;

        } else {
          // render a statement that there was a problem loading the results
          this.productFetchError = true;
          console.log(result.status)
        }
      })
  }

  displayMoreProducts() {
    this.fetchAllProducts();
  }

  updateFilteredProducts(filteredProducts: Product[]) {
    this.allProducts = filteredProducts;
  }

  // for opening the modal to the cart
  // the change in this variable is tracked in cart component, which reacts accordingly
  openModalCommand = false;
  toggleCartModal() {
    this.openModalCommand = !this.openModalCommand;
  }

  cart: Product[] = [];
  // adds clicked tile to cart
  addToCart(product: Product) {
    this.cart.push(product);
  }

  openProfileModalCommand = false;
  toggleProfileModal() {
    this.openProfileModalCommand = !this.openProfileModalCommand;
  }
}
