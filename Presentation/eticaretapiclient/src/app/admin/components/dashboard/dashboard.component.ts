import { Component } from '@angular/core';
import { AlertifyService, MessageType, Position } from '../../../services/admin/alertify.service';
import { BaseComponent, SpinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
 

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})

export class DashboardComponent extends BaseComponent {
  constructor(private alertify: AlertifyService , spinner:NgxSpinnerService) {
    super(spinner);

    this.showSpinner(SpinnerType.BallAtom,true)

  }
   
  m2() {
    this.alertify.message("Merhaba", {
      messageType: MessageType.Success,
      delay: 15,
      position: Position.BottomCenter
    });
  }

  d() {
    this.alertify.dismiss();
  }
}

