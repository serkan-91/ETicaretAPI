import { Component, EventEmitter, HostListener, Inject, Input, Output } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog'; 
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService, MessageType, Position } from '../../services/admin/alertify.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../services/ui/custom-toastr.service';
import { NgDialogAnimationService } from '../../services/common/ng-dialog-animation';
import { BaseComponent } from '../../base/base.component';
import { HttpClientService } from '../../services/common/http-client.service';
import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { DialogService, FileUploadOptions } from '../../services/common/dialog.service';



@Component({
  selector: 'app-file-upload-dialog',
  templateUrl: './file-upload-dialog.component.html',
  styleUrls: ['./file-upload-dialog.component.css']
})
export class FileUploadDialogComponent extends BaseComponent{
   
  constructor(
    private httpClientService : HttpClientService,
    private  alertifyService: AlertifyService,
    private toastrService: CustomToastrService,
    private dialog:NgDialogAnimationService,
    private dialogService: DialogService,
    spinner: NgxSpinnerService,
    dialogRef: MatDialogRef<FileUploadDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: FormData) {
    super(spinner  );
  }

  @Input() options: Partial<FileUploadOptions>;


  async onOkClick() {

   
       this.httpClientService.post({
         controller: this.options.controller,
         action: this.options.action,
         headers: new HttpHeaders({ "responseType": "blob" })
       }, this.data).subscribe({
         next: () => {
           this.dialogService.GetMessageForUpload({
             isAdminPage: true,
             message: "Delete transaction is successfully",
             status:true
           })
         },
         error: (errorResponse: HttpErrorResponse) => {
           this.dialogService.GetMessageForUpload({
             isAdminPage: true,
             message: "Delete transaction is successfully",
             status: false
           })
       
         }
       
       
       })

  }
   
  onNoClick() { }
}
export interface UploadDatas {
  Id: string,
  Element: HTMLElement,
  Controller: string
}

