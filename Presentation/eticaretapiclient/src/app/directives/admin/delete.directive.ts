import { Directive, ElementRef, Input, Renderer2, inject, HostListener, EventEmitter, Output } from '@angular/core';
import { DeleteDialogComponent, DeleteState } from '../../dialogs/delete-dialog/delete-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { HttpClientService } from '../../services/common/http-client.service';
import { AlertifyService } from '../../services/admin/alertify.service';
import { BaseComponent } from '../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { DialogService } from '../../services/common/dialog.service';
import { ChangeDetectorRef } from '@angular/core';

@Directive({
  selector: '[appDelete]',
})
export class DeleteDirective extends BaseComponent {
  @Input() id!: string; 
  @Input() controller?: string;  
  @Input() action?: string;  
  @Output() itemDeleted: EventEmitter<string> = new EventEmitter<string>();   

  readonly dialog = inject(MatDialog);
  constructor(
    private _element: ElementRef,
    private _httpClientService: HttpClientService,
    private _renderer: Renderer2,
    private _dialogService: DialogService,
    _spinner: NgxSpinnerService
  ) {
    super(_spinner);
    // İkonu oluştur ve elemente ekle
    const img = _renderer.createElement('img');
    img.setAttribute('src', '/assets/delete.png');
    img.setAttribute('style', 'cursor: pointer; transition: opacity 0.3s; opacity: 0.6;');
    img.width = 25;
    img.height = 25;
    _renderer.appendChild(_element.nativeElement, img);
  }

  @HostListener('click')
  async onClick() {
    this._dialogService.openDialog({
      componentType: DeleteDialogComponent,
      data: DeleteState.Yes,
      afterClosed: () => {
        this._httpClientService.delete({
          controller: this.controller,
          action: this.action
        }, this.id)
          .subscribe({
            next: () => {
              this.fadeOutRow();   
              this.itemDeleted.emit(this.id);   
            }
          });
      }
    });
  } 
  private fadeOutRow() {
    const rowElement = this._element.nativeElement.closest('mat-row');
    if (rowElement) {
      this._renderer.setStyle(rowElement, 'transition', 'opacity 0.5s ease');
      this._renderer.setStyle(rowElement, 'opacity', '0');

      setTimeout(() => {
        this._renderer.removeChild(rowElement.parentNode, rowElement);
      }, 500);
    }
  }

  @HostListener('mouseenter') onMouseEnter() {
    this.setOpacity(1);
  }

  @HostListener('mouseleave') onMouseLeave() {
    this.setOpacity(0.6);
  }

  private setOpacity(opacity: number) {
    const img = this._element.nativeElement.querySelector('img');
    if (img) {
      this._renderer.setStyle(img, 'opacity', opacity);
    }
  }
}
 
