import { Component } from '@angular/core';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from './services/ui/custom-toastr.service';
 

declare var   $: any

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'eticaretapiclient';
   
  constructor( ) {

    //toastrService.message("Serkan", "Prorammer", {
    //  messageType: ToastrMessageType.Info,
    //  position: ToastrPosition.TopCenter
    //  })
    //toastrService.message("Serkan", "Prorammer", {
    //  messageType: ToastrMessageType.Success,
    //  position: ToastrPosition.TopFullWidth
    //  })
    //toastrService.message("Serkan", "Prorammer", {
    //  messageType: ToastrMessageType.Warning,
    //  position: ToastrPosition.BottomLeft
    //  })
    //toastrService.message("Serkan", "Prorammer", {
    //  messageType: ToastrMessageType.Error,
    //  position: ToastrPosition.TopLeft
    //  }) 
  }

}

