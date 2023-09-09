import { Component } from '@angular/core';
import { BaseComponent, SpinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent extends BaseComponent{
  constructor(spinner: NgxSpinnerService) {
    super(spinner)

    this.showSpinner(SpinnerType.BallAtom,true)

  }
}
