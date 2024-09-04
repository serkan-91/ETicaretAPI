import { Component } from '@angular/core';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from './services/ui/custom-toastr.service';
  
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'eticaretapiclient';
   
  constructor(toastrService : CustomToastrService) {

     toastrService.message("Serkan", "Durgut", {
       messageType: ToastrMessageType.Info,
       position: ToastrPosition.TopRight
       })
    toastrService.message("Serkan", "Durgut", {
       messageType: ToastrMessageType.Success,
      position: ToastrPosition.TopRight
       })
     toastrService.message("Serkan", "Durgut", {
       messageType: ToastrMessageType.Warning,
       position: ToastrPosition.TopRight
       })
     toastrService.message("Serkan", "Durgut", {
       messageType: ToastrMessageType.Error,
       position: ToastrPosition.TopRight
       }) 
  }

}

