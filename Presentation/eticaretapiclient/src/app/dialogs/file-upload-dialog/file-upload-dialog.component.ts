import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BaseDialog } from '../base/base-dialogs';

@Component({
  selector: 'app-file-upload-dialog',
  templateUrl: './file-upload-dialog.component.html',
  styleUrl: './file-upload-dialog.component.css'
})
export class FileUploadDialogComponent extends BaseDialog<FileUploadDialogComponent> {
  constructor(dialogRef: MatDialogRef<FileUploadDialogComponent>,
   @Inject(MAT_DIALOG_DATA) public data: FileUploadState   )
  {
      super(dialogRef);
  }
}
 export enum FileUploadState {
   Yes,
   No
 }
