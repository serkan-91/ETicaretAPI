import { Component, ElementRef, Input } from '@angular/core';
import { FaIconService } from '../../services/common/fa-Icon.service';
import { BaseComponent } from '../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner'; 
import { DeleteDatas, DeleteDialogComponent } from '../../dialogs/delete-dialog/delete-dialog.component';
 

import { DialogService } from '../../services/common/dialog.service';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-delete-button',
  template: `
 <button mat-raised-button color="warn" (click)="openDeleteDialog()" >
  <mat-icon class="noTextMatIcon">
    <fa-icon [icon]="faXmark"></fa-icon>
  </mat-icon>
</button>

`,
  styleUrls: ['./delete-button.component.css'],

})

export class DeleteButtonComponent extends BaseComponent {
  
  constructor(
    spinner: NgxSpinnerService,
    private faIconService: FaIconService,
     private dialogService: DialogService,
    private element: ElementRef, 
  )
  {
    super(spinner );
  }

   deleteDatas: DeleteDatas;

  faXmark = this.faIconService.faXmark;
  
  @Input() controller: string;
  @Input() id: string;

  
  async openDeleteDialog() {

    const td: HTMLImageElement = this.element.nativeElement.parentElement
    const tr = td.parentElement

    this.deleteDatas = {
      Element: tr,
      Id: this.id,
      Controller: this.controller
    }

   this.dialogService.openDialog({
     componentType: DeleteDialogComponent,
     
     data: this.deleteDatas

   })
  }
  

}   
