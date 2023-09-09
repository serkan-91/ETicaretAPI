import { Component } from '@angular/core';
import { NgxSpinnerService, Spinner } from 'ngx-spinner';

 
export  class BaseComponent {

  constructor(private spinner: NgxSpinnerService) { }

  showSpinner(spinnerNameType: SpinnerType,isClose: boolean = false) {
    this.spinner.show(spinnerNameType)

    if (isClose) {
    setTimeout(() => this.hideSpinner(spinnerNameType), 1000);
    }
     
  }

  hideSpinner(spinnerNameType: SpinnerType) {
    this.spinner.hide(spinnerNameType);

 

  }

  }


export enum SpinnerType {
    BallAtom = "s1",
    BallScaleMultiple = "s2",
    BallSpinClockWiseFadeRotating = "s3",
     
  }

