import { Component, Input, IterableDiffers, OnInit, SimpleChanges } from '@angular/core';
import {Router, ActivatedRoute} from "@angular/router"
import { Product } from '../product-page/product-page.component';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})

export class CartComponent implements OnInit {
  constructor(private router: Router, private iterableDiffer: IterableDiffers, private route: ActivatedRoute) {
    this.route.queryParams.subscribe((params) => {
      try {
        let previousCart: Product[] = JSON.parse(params["cart"]);
        this.previousCart = previousCart;
        this.cartPriceTotal = this.cartTotal();
      } catch (ex) {
        console.log(ex);
      }
    })
  }

  @Input() previousUrl: string = '';

  // necessary to have another field, previousCart, in order to observe its changes & push to cart
  previousCart: Product[] = []
  ngOnInit(): void {
      this.previousCart.forEach((item) => this.cart.push(item))
  }

  // listening for changes to openModalCommand from parent (fires when user clicks cart icon to open modal)
  @Input() openModalCommand: boolean = false;
  ngOnChanges(changes: SimpleChanges) {
    try {
      if (changes["openModalCommand"].previousValue != changes["openModalCommand"].currentValue) {
        this.toggleCartModal();
      }
    } catch(ex: any) {

    }
  }

  @Input() cart: Product[] = [];
  ngDoCheck() {
    let changes = this.iterableDiffer.find(this.cart);
    if (changes) {
      this.cartPriceTotal = this.cartTotal();
    }
  }

  // cartToggle needs to be initially set to TRUE so that it does not automatically open on load:
  // The opening of the modal within parent product-page component fires an event that triggers toggleCartModal automatically
  cartToggle = true;
  toggleCartModal() {
    this.cartToggle = !this.cartToggle;
  }

  cartPriceTotal = 0;
  cartTotal() {
    let cartPriceTotal = 0;
    this.cart.forEach(cartItem => cartPriceTotal += cartItem.price)
    return cartPriceTotal;
  }

  makePurchase() {
    // send cart data to checkout page
    this.router.navigate(["/checkout"], {queryParams : {cart: JSON.stringify(this.cart)}});
  }

  removeCartItem(itemName: any) {
    let itemIndex = this.cart.indexOf(itemName);
    this.cart.splice(itemIndex, 1);
    // update cart total
    this.cartPriceTotal = this.cartTotal();
  }
}
