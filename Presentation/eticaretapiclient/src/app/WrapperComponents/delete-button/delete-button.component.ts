import { Component, ElementRef, EventEmitter, Input, Output } from '@angular/core';
import { ProductService } from '../../services/common/models/product.service';
import { BaseComponent } from '../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { FaIconService } from '../../services/common/fa-Icon.service';
import { findIconDefinition } from '@fortawesome/fontawesome-svg-core';
import { IconDefinition } from '@fortawesome/angular-fontawesome';
import { trigger, state, style, transition, animate } from '@angular/animations';

declare let $: any

@Component({
    selector: 'app-delete-button',
    template: `
 <button mat-icon-button color="warn" (click)="onDeleteClick()" >
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
        spinner: NgxSpinnerService
    ) {
        super(spinner);
  }
    faXmark: IconDefinition = findIconDefinition({ prefix: 'fas', iconName: 'xmark' });


  ngOnInit() 
  {
    this.faXmark = this.faIconService.faXmark;
  }

    @Input()
    id!: string;
    @Output() callback: EventEmitter<any> = new EventEmitter();

    async onDeleteClick() {
        const td: HTMLImageElement = this.element.nativeElement.parentElement
        const tr = td.parentElement

        //console.log(td)
        this.productService.delete(
            {
                id: td.id,
                successCallBack: () => $(tr).fadeOut(2000, () => {
                    this.callback.emit();
                }) /*, errorCallBack: () => { }*/
            });
    }
}
