import { Component } from '@angular/core';
import { BaseComponent, SpinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-baskets',
  templateUrl: './baskets.component.html',
  styleUrls: ['./baskets.component.css']
})
export class BasketsComponent extends BaseComponent{

  constructor(spinner: NgxSpinnerService) {
    super(spinner)

    this.showSpinner(SpinnerType.BallAtom,true)

  }
}
