import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductPageComponent } from './product-page.component';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing'; 


describe('ProductPageComponent', () => {
  let component: ProductPageComponent;
  let fixture: ComponentFixture<ProductPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductPageComponent ],
      imports: [RouterTestingModule, HttpClientTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should toggle cart modal', () => {
    component.toggleCartModal();
    expect(component.openModalCommand).toBe(true)
  });

  it('should toggle user profile modal', () => {
    component.toggleProfileModal();
    expect(component.openProfileModalCommand).toBe(true)
  });

  it('should add item: Product to cart', () => {
    const testProduct = {
      id: 1,
      imgURL: "https://image.com",
      name: "test product",
      price: 100,
      material: "gold",
      type: "ring"
    };

    component.addToCart(testProduct);
    expect(component.cart.length).toBe(1);
  });

  it('should update filtered items', () => {
    const testArray = []
    const testProduct = {
      id: 1,
      imgURL: "https://image.com",
      name: "test product",
      price: 100,
      material: "gold",
      type: "ring"
    };
    testArray.push(testProduct);

    component.updateFilteredProducts(testArray);
    expect(component.allProducts.length).toBe(1);
  })

});
