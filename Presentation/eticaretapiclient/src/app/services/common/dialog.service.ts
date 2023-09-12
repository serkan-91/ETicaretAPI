import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { NgDialogAnimationService } from './ng-dialog-animation';
import { ComponentType } from 'ngx-toastr/portal/portal';
import { DialogPosition, MatDialogRef } from '@angular/material/dialog';
import { AlertifyService, MessageType, Position } from '../admin/alertify.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../ui/custom-toastr.service';


@Injectable({
  providedIn: 'root',
})
export class DialogService {

  constructor(private dialog: NgDialogAnimationService,
    private alertifyService: AlertifyService,
    private customToastrService: CustomToastrService) { }


  private callbackGetListSubject = new Subject<void>();

  callbackProducts$ = this.callbackGetListSubject.asObservable();

  triggerGetProducts() {
    this.callbackGetListSubject.next();
  }

  openDialog(dialogParameters: Partial<DialogParameters>): void {
    const dialogRef = this.dialog.open(dialogParameters.componentType, {
      width: dialogParameters.options?.width,
      height: dialogParameters.options?.height,
      position: dialogParameters.options?.position,
      animation: {
        incomingOptions: {
          keyframes: [
            { opacity: 0 }, // Başlangıçta görünmez
            { opacity: 1 }, // Son halde tamamen görünür
          ],
          keyframeAnimationOptions: {
            easing: 'ease-in-out', // Hız eğrisi
            duration: 1000, // Animasyon süresi (milisaniye cinsinden)
          },
        },
        outgoingOptions: {
          keyframes: [
            { opacity: 1 }, // Başlangıçta tamamen görünür
            { opacity: 0 }, // Son halde görünmez
          ],
          keyframeAnimationOptions: {
            easing: 'ease-in-out', // Çıkış animasyonu hız eğrisi
            duration: 800, // Çıkış animasyonu süresi (milisaniye cinsinden)
          },
        },
      },

      data: dialogParameters.data,

    });

    dialogRef.afterClosed().subscribe(result => {
      if (result == dialogParameters.data)
        dialogParameters.afterClosed();
    });
  }


  closeDialog(dialogRef: MatDialogRef<any, any>, dialogParameters: Partial<DialogParameters>): void {
    // Perform any necessary animation or cleanup here before closing the dialog
    // Once animation or cleanup is complete, close the dialog
    dialogRef.close(dialogParameters.data);
  }

  GetMessageForUpload(ops: FileUploadOptions) {
    if (ops.isAdminPage) {
      this.alertifyService.message(ops.message,
        {
          dismissOthers: true,
          messageType: ops.status ? MessageType.Success : MessageType.Error,
          position: Position.TopRight
        })
    }
    else {
      this.customToastrService.message(ops.message, "Başarılı.", {
        messageType: ops.status ? ToastrMessageType.Success : ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      })
    }

  }
}
export class DialogParameters {
  componentType: ComponentType<any>;
  data: any;
  afterClosed: () => void;
  options?: Partial<DialogOptions> = new DialogOptions();
}

export class DialogOptions {
  width?: string = "250px";
  height?: string;
  position?: DialogPosition;
}
export class FileUploadOptions {
  controller?: string;
  action?: string;
  queryString?: string;
  explanation?: string;
  accept?: string;
  isAdminPage?: boolean = false;
  message?: string
  status?: boolean = false
}

export class StatusMessage {
  SuccessMessage: string;
  ErrorMessage: string;
}
