import { Injectable } from '@angular/core';

declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {
  message(message: string, options: Partial<AlertifyOptions>): void {
    alertify.set('notifier', 'delay', options.delay);
    alertify.set('notifier', 'position', options.position);
    const msg = alertify[options.messageType || MessageType.Notify] as (msg: string) => void;

    if (options.dismissOthers) {
      alertify.dismissAll();
    }

    msg(message);
  }

  dismiss(): void {
    alertify.dismissAll();
  }
}

export enum MessageType {
  Error = 'error',
  Message = 'message',
  Notify = 'notify',
  Success = 'success',
  Warning = 'warning'
}

export enum Position {
  TopRight = 'top-right',
  TopCenter = 'top-center',
  TopLeft = 'top-left',
  BottomRight = 'bottom-right',
  BottomCenter = 'bottom-center',
  BottomLeft = 'bottom-left'
}

export class AlertifyOptions {
  messageType: MessageType = MessageType.Message;
  position: Position = Position.BottomLeft;
  delay: number = 5;
  dismissOthers: boolean = false;
}
