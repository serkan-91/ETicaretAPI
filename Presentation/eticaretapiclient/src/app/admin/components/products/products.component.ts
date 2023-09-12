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
 
  constructor(spinner: NgxSpinnerService  ) {
    super(spinner)
    
}
   

      @ViewChild(ListComponent) ListComponent : ListComponent

 
  createdProduct(createdProduct: Create_Product)
  {
    this.ListComponent.GetProducts() 
  }


}

