import { Injectable } from '@angular/core';
import {   FileUploadState } from '../../dialogs/file-upload-dialog/file-upload-dialog.component';
import { DialogPosition, MatDialog } from '@angular/material/dialog';
import { ComponentType } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private _dialog : MatDialog) { }

    openDialog(dialogParameters :  Partial<DialogParameters>): void {
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
        if (result == FileUploadState.Yes) {
          dialogParameters.afterClosed();
        }
      })
  }

}
export class DialogParameters {
  enterAnimationDuration?: any = 500;
  exitAnimationDuration?: any = 500;
  componentType: ComponentType<any>;
  data: any;
  afterClosed: () => void;
  options?: Partial<DialogOptions> = new DialogOptions();
}
export class DialogOptions {
  width?: string = "250px";
  height?: string;
  minWidth?: string ;
  position?: DialogPosition;

}
