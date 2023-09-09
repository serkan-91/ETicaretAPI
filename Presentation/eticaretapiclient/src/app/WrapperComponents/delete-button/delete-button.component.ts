import { Component, ElementRef, EventEmitter, Input, Output } from '@angular/core';
import { FaIconService } from '../../services/common/fa-Icon.service';
import { ProductService } from '../../services/common/models/product.service';
import { BaseComponent, SpinnerType } from '../../base/base.component';
import {  NgxSpinnerService } from 'ngx-spinner';

declare var $: any
@Component({
  selector: 'app-delete-button',
  template: `
 <button mat-raised-button color="warn" (click)="onDeleteClick()" >
  <mat-icon class="noTextMatIcon">
    <fa-icon [icon]="faXmark"></fa-icon>
  </mat-icon>
</button>

`,
  styleUrls: ['./delete-button.component.css'],
   
})

export class DeleteButtonComponent extends BaseComponent {
 

  constructor(
    private faIconService: FaIconService,
    private element: ElementRef,
    private productService: ProductService,
    spinner : NgxSpinnerService
  ) {
    super(spinner);
  }
  faXmark = this.faIconService.faXmark;

  @Input() id: string;
  @Output() callback: EventEmitter<any> = new EventEmitter();

  async onDeleteClick() {
    this.showSpinner(SpinnerType.BallAtom)
     const td: HTMLImageElement = this.element.nativeElement.parentElement
     const tr = td.parentElement

     //console.log(td)
     await this.productService.delete(td.id);

     $(tr).fadeOut(2000, () => {
       this.callback.emit();
     })
     
     
  }
}
