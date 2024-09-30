import {  Component, ViewChild } from '@angular/core';  
import { ListComponent } from './list/list.component'; 
import { Product } from '../../../contracts/list_product';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent    {
  @ViewChild(ListComponent) listComponent!: ListComponent;
  addProduct(newProduct: Product) {
    console.log(newProduct)
    this.listComponent.addProduct(newProduct);
  }
    
  }    

