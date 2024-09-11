import { Component, Inject } from '@angular/core'; 
import { MatDialogRef, MAT_DIALOG_DATA  } from '@angular/material/dialog';
import { BaseDialog } from '../base/base-dialogs';

@Component({
  selector: 'app-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrl: './delete-dialog.component.css'
})
export class DeleteDialogComponent extends BaseDialog<DeleteDialogComponent> {
  constructor(
    dialogRef: MatDialogRef<DeleteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DeleteState
  ) {
    super(dialogRef);
  }
}

export enum DeleteState {
  Yes,
  No
}
