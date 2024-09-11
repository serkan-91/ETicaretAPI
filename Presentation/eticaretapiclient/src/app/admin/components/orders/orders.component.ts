import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from '../../../base/base.component';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent extends BaseComponent {
  constructor(spinner: NgxSpinnerService) {
    super(spinner);
    this.showSpinner(SpinnerType.BallAtom)
  }
}
