import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from '../../../base/base.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent extends BaseComponent {
  constructor(spinner: NgxSpinnerService) {
    super(spinner)

    this.showSpinner({
      spinnerNameType: SpinnerType.BallAtom,
      IsClose: true  
    })

  }
}
