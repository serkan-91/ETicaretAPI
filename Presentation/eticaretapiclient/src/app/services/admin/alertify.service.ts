import { Injectable } from '@angular/core';
declare var alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {
  [x: string]: any;
  success(arg0: string) {
      throw new Error('Method not implemented.');
  }
  error(arg0: string) {
      throw new Error('Method not implemented.');
  }
  constructor() { }

  // message(message: string, messageType: MessageType, position: Position, delay: number = 3, dismissOthers: boolean = false)
  message(message: string, options: Partial<AlertifyOptions>) {
    const delay = options.delay ?? 3;  
    const position = options.position ?? Position.BottomCenter;  
    const messageType = options.messageType ?? 'message';  

    alertify.set('notifier', 'delay', delay);
    alertify.set('notifier', 'position', position); 
    const msj = alertify[messageType](message);
    if (options.dismissOthers)
      msj.dismissOthers();

  }

  dissmiss() {
    alertify.dismissAll();
  }
}

export class AlertifyOptions {
  messageType: MessageType = 'message';
  position: Position = Position.BottomLeft;
  delay: number = 3;
  dismissOthers: boolean = false;
}

export type MessageType = 'error' | 'message' | 'notify' | 'success' | 'warning'
 
    

export enum Position {
  TopCenter = "top-center",
  TopRight = "top-right",
  TopLeft = "top-left",
  BottomRight = "bottom-right",
  BottomCenter = "bottom-center",
  BottomLeft = "bottom-left"
}
