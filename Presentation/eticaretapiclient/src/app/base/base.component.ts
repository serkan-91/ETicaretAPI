 
import { NgxSpinnerService, Spinner } from 'ngx-spinner';


export class BaseComponent {

  constructor(private spinner: NgxSpinnerService) { }

  showSpinner(spinnerOptions: SpinnerOptions) {
    this.spinner.show(spinnerOptions.spinnerNameType)

    if (spinnerOptions.IsClose) {
      setTimeout(() => this.hideSpinner(spinnerOptions.spinnerNameType), spinnerOptions.Delay);
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
export class SpinnerOptions {
  spinnerNameType: SpinnerType
  IsClose?: boolean = false
  Delay?: number = 1000


}
