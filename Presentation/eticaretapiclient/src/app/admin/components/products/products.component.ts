import { Component, OnInit, ViewChild } from '@angular/core';
import { BaseComponent } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ListComponent } from './list/list.component';
  
@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent extends BaseComponent implements OnInit {
  constructor(spinner: NgxSpinnerService) {
    super(spinner) 
  }
    ngOnInit(): void {
    } 
      @ViewChild(ListComponent) ListComponent : ListComponent
       
  createdProduct()
  {
    this.ListComponent.GetProducts() 
  }
}
 
