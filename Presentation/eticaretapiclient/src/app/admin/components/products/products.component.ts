import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { ListComponent } from './list/list.component';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements AfterViewInit    {
  constructor( ) {
    
  } 
  @ViewChild(ListComponent) ListComponent?: ListComponent

  ngAfterViewInit() {
    // ngAfterViewInit içinde ListComponent'e güvenle erişilebilir
    if (this.ListComponent) {
      this.ListComponent.GetProducts();
    }
  }
  createdProduct() {
    if(this.ListComponent) {
      this.ListComponent.GetProducts();
    } else {
      console.error('ListComponent is not initialized or available yet.');
    }
  }
}
