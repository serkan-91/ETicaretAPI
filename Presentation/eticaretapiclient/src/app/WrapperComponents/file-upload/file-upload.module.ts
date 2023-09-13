import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileUploadComponent } from './file-upload.component';
import { NgxFileDropModule } from 'ngx-file-drop';
import { DialogModule } from '../../dialogs/dialog.module';
import { FileUploadDialogComponent } from '../../dialogs/file-upload-dialog/file-upload-dialog.component';
import { MatButtonModule } from '@angular/material/button';



@NgModule({
  declarations: [
    FileUploadComponent,FileUploadDialogComponent
  ],
  imports: [
    CommonModule,
    NgxFileDropModule,
    DialogModule, MatButtonModule
  ],
  exports: [
    FileUploadComponent
  ]
})
export class FileUploadModule { }