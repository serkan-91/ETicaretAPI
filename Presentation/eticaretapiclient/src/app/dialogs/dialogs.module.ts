import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { FileUploadDialogComponent } from './file-upload-dialog/file-upload-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { SelectProductImageDialogComponent } from './select-product-image-dialog/select-product-image-dialog.component';
import { FileUploadComponent } from '../services/common/file-upload/file-upload.component';
import { NgxFileDropModule } from 'ngx-file-drop';

@NgModule({
  declarations: [
    DeleteDialogComponent,
    FileUploadDialogComponent,
    SelectProductImageDialogComponent,
    FileUploadComponent

  ],
  imports: [
    CommonModule,
    MatDialogModule,
    MatButtonModule,
    NgxFileDropModule,
    MatFormFieldModule,
  ]
})
export class DialogsModule { }
