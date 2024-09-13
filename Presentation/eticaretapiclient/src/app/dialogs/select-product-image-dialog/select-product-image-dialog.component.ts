import { Component, Inject, Output } from '@angular/core';
import { BaseDialog } from '../base/base-dialogs';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FileUploadOptions } from '../../services/common/file-upload/file-upload.component';

@Component({
  selector: 'app-select-product-image-dialog',
  templateUrl: './select-product-image-dialog.component.html',
  styleUrl: './select-product-image-dialog.component.css'
})
export class SelectProductImageDialogComponent extends BaseDialog<SelectProductImageDialogComponent> {
  constructor(
    _dialogRef: MatDialogRef<SelectProductImageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SelectProductImageState
  ) {
    super(_dialogRef);
  }
  @Output() options: Partial<FileUploadOptions> = {
    accept: ".png, .jpg, .jpeg, .gif, .bmp",
    action: "Upload",
    controller: "Products",
    explanation: "Choose a product image or drag here a product image",
    isAdminPage: true,
    queryString: `id=${this.data}`
}
}

export enum SelectProductImageState {
  Close
}
