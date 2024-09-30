import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { AlertifyService, MessageType, Position } from '../../admin/alertify.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { NgxFileDropEntry } from 'ngx-file-drop';
import { DialogService } from '../dialog.service';
import { FileUploadDialogComponent, FileUploadState } from '../../../dialogs/file-upload-dialog/file-upload-dialog.component';
import { SpinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs/internal/Observable';
import { ProductImage } from '../../../contracts/list_product_image'; 

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent   {

  constructor(
    private _httpClientService: HttpClientService,
    private _alertifyService: AlertifyService,
    private _toastrService: CustomToastrService,
    private _dialogService: DialogService,
    private _spinner: NgxSpinnerService,
  ) { }

  public files: NgxFileDropEntry[] = [];
  public errorMessage: string = '';
  private maxFileSize = 20 * 1024 * 1024; // 20 MB


  @Input() options?: Partial<FileUploadOptions>;
  @Output() fileUploaded: EventEmitter<ProductImage[]> = new EventEmitter<ProductImage[]>();
     
    selectedFiles(files: NgxFileDropEntry[]) {
    this.files = files;
    const fileData: FormData = new FormData();
    let hasFileSizeError = false;

    for (const file of files) {
      (file.fileEntry as FileSystemFileEntry).file((_file: File) => {
        if (_file.size > this.maxFileSize) {

          this.errorMessage = `${_file.name} cannot be bigger than 20 MB.`;
          hasFileSizeError = true;
          this._spinner.hide(SpinnerType.BallAtom);
          const message: string = `${_file.name} cannot be bigger than 20 MB `;
          if (this.options?.isAdminPage) {
            this._alertifyService.message(message, {
              dismissOthers: true,
              messageType: 'success',
              position: Position.TopRight
            });
          } else {
            this._toastrService.message(message, 'Error', {
              messageType: ToastrMessageType.Error,
              position: ToastrPosition.TopRight
            });
          }
        } else {
          fileData.append(_file.name, _file, file.relativePath);  
          this.errorMessage = '';
        }
      });
    }

    // Eğer dosya boyutu hatası varsa dialog açılmasın
    if (hasFileSizeError) {
      return; // İşlemi iptal et
    }

    this._dialogService.openDialog({
      componentType: FileUploadDialogComponent,
      data: FileUploadState.Yes,
      afterClosed: () => {
        this._httpClientService.post<FormData, ProductImage[]>({
          controller: this.options?.controller,
          action: this.options?.action,
          queryString: `id=${this.options?.queryString}`
        }, fileData)
          .subscribe({
            next: (data: ProductImage[]) => {
            const message: string = 'Files have been uploaded successfully.';
            this.fileUploaded.emit(data)
            if (this.options?.isAdminPage) {
              this._alertifyService.message(message, {
                dismissOthers: true,
                messageType: 'success',
                position: Position.TopRight
              });
            }
            else {
              this._toastrService.message(message, 'Success', {
                messageType: ToastrMessageType.Success,
                position: ToastrPosition.TopRight
              });
            }
          }
        });
      }
    });
  }
  images$?: Observable<ProductImage[]>;
}

export class FileUploadOptions {
  controller?: string;
  action?: string;
  queryString?: string;
  explanation?: string;
  accept?: string;
  isAdminPage?: boolean = false;
}
