import { MatDialogRef } from '@angular/material/dialog';

export class BaseDialog<DialogComponent> {
  constructor(public _dialogRef: MatDialogRef<DialogComponent>) { }
  close() {
    this._dialogRef.close();
  }
}
