import { Component, ViewChild } from '@angular/core';
import { BaseComponent, SpinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { HttpClientService } from '../../../services/common/http-client.service';
import { Create_Product } from '../../../contracts/create_product';
import { ListComponent } from './list/list.component';
 


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent extends BaseComponent {
  constructor(spinner: NgxSpinnerService, httpClientService: HttpClientService) {
    super(spinner) 
    this.showSpinner(SpinnerType.BallAtom,true) 
  } 

      @ViewChild(ListComponent) ListComponent : ListComponent
       
  createdProduct(createdProduct: Create_Product)
  {
    this.ListComponent.GetProducts() 
  }
}





  
    //httpClientService.get<Product[]>({
    //  controller: "products"
    //}).subscribe(data => console.log(data));

    //httpClientService.post({
    //  controller: "products"
    //}, {
    //  name: "Defter",
    //  stock: 1001,
    //  price: 11
    //}).subscribe();

    //httpClientService.put({
    //  controller: "products"
    //},
    //  {
    //    id: "17cd9b83-bae1-4de2-97af-1845672e3a3e",
    //    Name: "Renkli Kagit",
    //    stock: 1500,
    //    price:5.5

    //}).subscribe()

    //httpClientService.delete(
    //  {
    //   controller: "products"
    //  },
    //  "c5379e80-0441-4c4a-b8ed-c74b2a5c39d0")
    //  .subscribe();
      
