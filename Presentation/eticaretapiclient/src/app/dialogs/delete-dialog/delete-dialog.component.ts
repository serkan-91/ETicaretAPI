import { Component,   Inject     } from '@angular/core';
import { MAT_DIALOG_DATA,   MatDialogRef } from '@angular/material/dialog';

import {  BaseComponent, SpinnerType } from '../../base/base.component';
import { DialogService, StatusMessage } from '../../services/common/dialog.service';
import { HttpClientService } from '../../services/common/http-client.service';
import { AlertifyService, MessageType, Position } from '../../services/admin/alertify.service';
import { HttpErrorResponse } from '@angular/common/http';
 import { NgxSpinnerService } from 'ngx-spinner';
 
declare var $: any


@Component({
  selector: 'app-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrls: ['./delete-dialog.component.css']
})



export class DeleteDialogComponent extends   BaseComponent 
{
  
  constructor( 
    spinner: NgxSpinnerService,
    private client: HttpClientService,
    private dialogRef: MatDialogRef<DeleteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DeleteDatas,
    private dialogService: DialogService,
    private alertifyService: AlertifyService,
  ) {
    super(spinner );

  }

   
  statusMessage: StatusMessage = {
    SuccessMessage: null,
    ErrorMessage: null
  }
   

  async onOkClick() {
    
    this.showSpinner({
      spinnerNameType: SpinnerType.BallAtom,
      IsClose: false
    })

    switch (this.data.Controller) {
      case 'products':
        this.statusMessage = {
          SuccessMessage: "This Product has been deleted",
          ErrorMessage: "While deleted this product occurred an error "
        }


    }

    this.client.delete({
      controller: this.data.Controller
    },
      this.data.Id)
      .subscribe({
        next:
          () => {
            $(this.data.Element).fadeOut(500, async () => {

              this.hideSpinner(SpinnerType.BallAtom)

              this.dialogService.triggerGetProducts();
              this.alertifyService.message(this.statusMessage.SuccessMessage,
                {
                  dismissOthers: true,
                  messageType: MessageType.Success,
                  position: Position.TopRight
                })
              this.dialogRef.close();
            })
          },
        error:

          (errorResponse: HttpErrorResponse) => {

            this.hideSpinner(SpinnerType.BallAtom)

            this.alertifyService.message(this.statusMessage.ErrorMessage,
              {
                dismissOthers: true,
                messageType: MessageType.Error,
                position: Position.TopRight
              })
            this.dialogRef.close();


          }

      })
  }

  onNoClick() {
     // Replace with the actual method to get the dialog reference
    
    this.dialogRef.close();
  }

  } 


export interface DeleteDatas {
  Id: string,
  Element: HTMLElement,
  Controller: string
}

