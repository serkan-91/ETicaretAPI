import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { FileUploadDialogComponent } from './file-upload-dialog/file-upload-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FileUploadModule } from '../WrapperComponents/file-upload/file-upload.module';
import { DeleteButtonComponent } from '../WrapperComponents/delete-button/delete-button.component';
 



@NgModule({
  declarations: [
    DeleteDialogComponent    ],
  imports: [
    CommonModule,
    MatDialogModule, MatButtonModule, MatTableModule, MatListModule, MatFormFieldModule, MatInputModule, MatInputModule
  ] 
})
export class DialogModule { }
