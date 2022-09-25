import { Component, Input, Output, EventEmitter, ElementRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Product} from "../product-page/product-page.component";
import { Router } from '@angular/router';
import { ApiService } from '../api.service';

export interface ActiveFilters {
  type: string,
  material: string
};

@Component({
  selector: 'app-filters',
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.css']
})

export class FiltersComponent {

  constructor(private http: HttpClient, private router: Router, private elem: ElementRef, private service: ApiService) { }

  @Input() accessToken: string = '';

  currentFilters: ActiveFilters = {
    type: 'None',
    material: 'None'
  }

  activateFilter(event: any, category: string, detail: string) {
    // reset active class of all
    if (event.target.classList.contains("filter-option-type")) {
      const elements = this.elem.nativeElement.querySelectorAll(".filter-option-type")
      elements.forEach((element: any) => {
        element.classList.remove("active-filter");
      });

    } else if (event.target.classList.contains("filter-option-material"))  {
        const elements = this.elem.nativeElement.querySelectorAll(".filter-option-material")
        elements.forEach((element: any) => {
          element.classList.remove("active-filter");
        });
    }

    let clickedFilter = event.target;
    clickedFilter.classList.toggle("active-filter");

    if (category === 'type') {
      this.currentFilters.type = detail;

      this.filterProducts();

    } else if (category === 'material') {
      this.currentFilters.material = detail;

      this.filterProducts();
    }
  }

  // output to be listened for on product-page
  // should be of type Product[], but linter won't recognize it
  @Output() updatedProducts = new EventEmitter<any>();
  filterProducts() {
    this.service.getFilteredProducts(this.currentFilters.material, this.currentFilters.type, this.accessToken)
      .subscribe((result) => {
        // need to subscribe to changes to filteredProducts & change allProducts in product-page accordingly
        this.updatedProducts.emit(result.body)
      })
  }

  resetFilters() {
    // reset tracker
    this.currentFilters.type = 'None';
    this.currentFilters.material = 'None';

    const materials = this.elem.nativeElement.querySelectorAll(".filter-option-material");
    const types = this.elem.nativeElement.querySelectorAll(".filter-option-type");

    materials.forEach((element: any) => {
      element.classList.remove("active-filter");
    });
    types.forEach((element: any) => {
      element.classList.remove("active-filter");
    });

    this.fetchAllProducts();
  }

  allProducts: Array<Product> = [];

  startRow: number = 0;
  endRow: number = 8;
  fetchAllProducts() {
    this.service.getProducts(this.startRow, this.endRow, this.accessToken)
      .subscribe((result: any) => {
        this.updatedProducts.emit(result.body)
      })
  }
}
