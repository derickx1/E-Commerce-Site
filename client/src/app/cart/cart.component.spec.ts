import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CartComponent } from './cart.component';

import { RouterTestingModule } from '@angular/router/testing';

import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CartComponent', () => {
  let component: CartComponent;
  let fixture: ComponentFixture<CartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CartComponent ],
      imports: [RouterTestingModule, HttpClientTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should toggle modal', () => {
    component.toggleCartModal();
    expect(component.cartToggle).toBe(false);
  });

  it('should remove item from cart', () => {
    component.cart = [
      {
        id: 1,
        imgURL: "testUrl",
        name: "item1",
        price: 100,
        material: "testMaterial",
        type: "testType"
      },
      {
        id: 2,
        imgURL: "testUrl",
        name: "item2",
        price: 100,
        material: "testMaterial",
        type: "testType"
      }
    ]

    component.removeCartItem("item2");
    expect(component.cart.length).toBe(1);
  });

  it('should update cart price total', () => {
    component.cart = [
      {
        id: 1,
        imgURL: "testUrl",
        name: "item1",
        price: 100,
        material: "testMaterial",
        type: "testType"
      },
      {
        id: 2,
        imgURL: "testUrl",
        name: "item2",
        price: 100,
        material: "testMaterial",
        type: "testType"
      }
    ];

    const priceTotal: number = component.cartTotal();
    expect(priceTotal).toBe(200);
  })

});
