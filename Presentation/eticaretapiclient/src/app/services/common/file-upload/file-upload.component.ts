import { Component, EventEmitter, Input, Output } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { AlertifyService, MessageType, Position } from '../../admin/alertify.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { NgxFileDropEntry } from 'ngx-file-drop';
import { DialogService } from '../dialog.service';
import { FileUploadDialogComponent, FileUploadState } from '../../../dialogs/file-upload-dialog/file-upload-dialog.component';
import { HttpHeaders } from '@angular/common/http';
import { SpinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs/internal/Observable';
import { List_Product_Image } from '../../../contracts/list_product_image';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent {
  constructor(private _httpClientService: HttpClientService,
    private _alertifyService: AlertifyService,
    private _toastrService: CustomToastrService,
    private _dialogService: DialogService,
    private _spinner: NgxSpinnerService) { }

  public files: NgxFileDropEntry[] = [];
  public errorMessage: string = '';
  private maxFileSize = 20 * 1024 * 1024; // 20 MB

  @Input() options: Partial<FileUploadOptions>;
  @Output() fileUploaded: EventEmitter<void> = new EventEmitter<void>(); // Dosya yüklendikten sonra tetiklenecek olay

  public selectedFiles(files: NgxFileDropEntry[]) {
    this.files = files;
    const fileData: FormData = new FormData();
    let hasFileSizeError = false;

    // Dosya boyutlarını kontrol et
    for (const file of files) {
      (file.fileEntry as FileSystemFileEntry).file((_file: File) => {
        if (_file.size > this.maxFileSize) {
          // Eğer dosya boyutu sınırdan büyükse hata mesajı göster
          this.errorMessage = `${_file.name} cannot be bigger than 20 MB.`;
          hasFileSizeError = true;
          this._spinner.hide(SpinnerType.BallAtom); // Spinner kapatılıyor
          const message: string = `${_file.name} cannot be bigger than 20 MB `;
          // Kullanıcı admin ise Alertify, değilse Toastr kullan
          if (this.options.isAdminPage) {
            this._alertifyService.message(message, {
              dismissOthers: true,
              messageType: MessageType.Error,
              position: Position.TopRight
            });
          } else {
            this._toastrService.message(message, "Error", {
              messageType: ToastrMessageType.Error,
              position: ToastrPosition.TopRight
            });
          }
        } else {
          // Dosya boyutu uygun, FormData'ya ekle
          fileData.append(_file.name, _file, file.relativePath);
          this.errorMessage = ''; // Hata mesajını temizle
        }
      });
    }

    // Eğer dosya boyutu hatası varsa dialog açılmasın
    if (hasFileSizeError) {
      return; // İşlemi iptal et
    }

    // Dosya boyutları uygunsa dialog aç ve dosya yükle
    this._dialogService.openDialog({
      componentType: FileUploadDialogComponent,
      data: FileUploadState.Yes,
      afterClosed: () => {
        this._spinner.show(SpinnerType.BallAtom);
        this._httpClientService.post({
          controller: this.options.controller,
          action: this.options.action,
          queryString: this.options.queryString,
          headers: new HttpHeaders({ "responseType": "blob" })
        }, fileData).subscribe({
          next: () => {
            const message: string = "Files have been uploaded successfully.";
            this.fileUploaded.emit(); // Dosya başarıyla yüklendiğinde olayı tetikle
            if (this.options.isAdminPage) {
              this._alertifyService.message(message, {
                dismissOthers: true,
                messageType: MessageType.Success,
                position: Position.TopRight
              });
            } else {
              this._toastrService.message(message, "Başarılı", {
                messageType: ToastrMessageType.Success,
                position: ToastrPosition.TopRight
              });
            }
            this._spinner.hide(SpinnerType.BallAtom);
          },
          error: () => {
            const message: string = "An unexpected error was encountered while uploading files.";
            this._spinner.hide(SpinnerType.BallAtom);

            if (this.options.isAdminPage) {
              this._alertifyService.message(message, {
                dismissOthers: true,
                messageType: MessageType.Error,
                position: Position.TopRight
              });
            } else {
              this._toastrService.message(message, "Unsuccessful", {
                messageType: ToastrMessageType.Error,
                position: ToastrPosition.TopRight
              });
            }
          }
        });
      }
    });
  }
  images$: Observable<List_Product_Image[]>;
}

export class FileUploadOptions {
  controller?: string;
  action?: string;
  queryString?: string;
  explanation?: string;
  accept?: string;
  isAdminPage?: boolean = false;
}
