import { Injectable } from '@angular/core';
import {   FileUploadState } from '../../dialogs/file-upload-dialog/file-upload-dialog.component';
import { DialogPosition, MatDialog } from '@angular/material/dialog';
import { ComponentType } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private _dialog : MatDialog) { }

  openDialog(dialogParameters: Partial<DialogParameters> ): void {
    if (!dialogParameters.componentType) {
      throw new Error('componentType is required');
    }
    const dialogRef = this._dialog.open(dialogParameters.componentType, 
      {
        width:  dialogParameters.options?.width,
        height: dialogParameters.options?.height,
        minWidth: dialogParameters.options?.minWidth,
        position: dialogParameters.options?.position,
        enterAnimationDuration: dialogParameters.enterAnimationDuration,
        exitAnimationDuration: dialogParameters.exitAnimationDuration,
        data: dialogParameters.data
      });

    dialogRef.afterClosed()
      .subscribe((result: FileUploadState) => {
        if (result === FileUploadState.Yes && dialogParameters.afterClosed) {
          dialogParameters.afterClosed();
        }
      })
  }

}
export class DialogParameters<T = unknown> {
  enterAnimationDuration: number = 500;
  exitAnimationDuration: number = 500;
  componentType!: ComponentType<T>;
  data: unknown;
  afterClosed: (() => void) | undefined;
  options?: Partial<DialogOptions> = new DialogOptions();
}
export class DialogOptions {
  width?: string = '250px';
  height?: string;
  minWidth?: string ;
  position?: DialogPosition;

}
