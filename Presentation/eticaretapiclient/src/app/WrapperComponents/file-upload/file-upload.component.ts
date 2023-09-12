import { Component, Input,   } from '@angular/core';  
import { MatDialogRef } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgxFileDropEntry } from 'ngx-file-drop';
import { FileUploadDialogComponent } from '../../dialogs/file-upload-dialog/file-upload-dialog.component';
import { DialogService } from '../../services/common/dialog.service';
import { BaseComponent } from '../../base/base.component';
@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent extends BaseComponent {
  constructor(
 
    private dialogService: DialogService,

    dialogRef : MatDialogRef<FileUploadComponent>,
    spinner : NgxSpinnerService
  ) {
    super(spinner);
  }

  @Input() options: Partial<FileUploadOptions>;

  // Seçilen dosyaları saklamak için bir değişken tanımlıyoruz.
  public files: NgxFileDropEntry[];
  
  
  // Kullanıcının seçtiği dosyaları işlemek için bir fonksiyon tanımlıyoruz.
  public  async selectedFiles(files: NgxFileDropEntry[]) {

    this.files = files;

    // Dosyaları bir FormData nesnesine ekliyoruz.
    const fileData: FormData = new FormData();

    for (const file of files) {
      // Dosya, bir FileSystemFileEntry ise dosyayı FormData'ya ekliyoruz.
      (file.fileEntry as FileSystemFileEntry).file((_file: File) =>
      {

        fileData.append(_file.name, _file, file.relativePath);
      })
    
    }

    this.dialogService.openDialog( {
      componentType: FileUploadDialogComponent,
      data: fileData,
      
    })

   
       
  }
  }

  
export class FileUploadOptions {
  controller?: string;
  action?: string;
  queryString?: string;
  explanation?: string;
  accept?: string;
  isAdminPage?: boolean = false;
}
