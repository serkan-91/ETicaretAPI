import { Injectable } from '@angular/core';
import {   ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class CustomToastrService {
  constructor(private toastr: ToastrService) { }

  message(
      message: string,
      title: string,
      toastrOptions: Partial<ToastrOptions>)
  {
    const messageType = toastrOptions.messageType ?? ToastrMessageType.Success;
    const position = toastrOptions.position ?? ToastrPosition.TopRight;

    this.toastr[messageType](message, title, { positionClass: position });
    } 
} 

export interface ToastrOptions {
  messageType: ToastrMessageType;
  position: ToastrPosition;
}
export enum ToastrMessageType {
  Success = 'success',
  Info = 'info',
  Warning = 'warning',
  Error = 'error'
}
export enum ToastrPosition {
  TopRight = 'toast-top-right',
  BottomRight = 'toast-bottom-right',
  BottomLeft = 'toast-bottom-left',
  TopLeft = 'toast-top-Left',
  TopFullWidth = 'toast-top-full-width',
  BottomFullWidth = 'toast-bottom-full-width',
  TopCenter = 'toast-top-center',
  BottomCenter = 'toast-bottom-center'
}
