import { NgxSpinnerService } from 'ngx-spinner';

export class BaseComponent {
  constructor(private spinner: NgxSpinnerService) { }

  showSpinner(spinnerNameType: SpinnerType) {
    this.spinner.show(spinnerNameType);
  }
  hideSpinner(spinnerNameType: SpinnerType, delay: number = 500) {
    setTimeout(() => {
      this.spinner.hide(spinnerNameType);
    }, delay);  // Belirtilen süre kadar gecikme ekler
  }
}

export enum SpinnerType {
  BallAtom = 's1',
  BallScaleMultiple = 's2',
  BallSpinClockwiseFadeRotating = 's3'
}
