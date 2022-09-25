import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginPageComponent} from "./login-page/login-page.component";
import {ProductPageComponent} from "./product-page/product-page.component";
import {CheckoutPageComponent} from "./checkout-page/checkout-page.component";

const routes: Routes = [
  {path: "", component: LoginPageComponent},
  {path: "productPage", component: ProductPageComponent},
  {path: "checkout", component: CheckoutPageComponent}
]; // where routes are defined

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
