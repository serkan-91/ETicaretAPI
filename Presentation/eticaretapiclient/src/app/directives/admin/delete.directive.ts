import { Directive, ElementRef, EventEmitter, HostListener, Input, Output, Renderer2, ViewChild, inject } from '@angular/core'; 
import { DeleteDialogComponent, DeleteState } from '../../dialogs/delete-dialog/delete-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { HttpClientService } from '../../services/common/http-client.service';
import { AlertifyService, MessageType, Position } from '../../services/admin/alertify.service';
import { HttpErrorResponse } from '@angular/common/http';
import { BaseComponent, SpinnerType } from '../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { DialogService } from '../../services/common/dialog.service';

@Directive({
  selector: '[appDelete]',
})

export class DeleteDirective extends BaseComponent {
  @Input() color: string = 'primary'; // Varsayılan renk
  readonly dialog = inject(MatDialog);

  constructor(
    private _element: ElementRef,
    private _httpClientService: HttpClientService,
    private _renderer: Renderer2,
    private _alertify: AlertifyService,
    private _dialogService: DialogService,
    _spinmner: NgxSpinnerService

  ) {
    super(_spinmner);
    const img = _renderer.createElement("img");
    img.setAttribute("src", "/assets/delete.png");
    img.setAttribute("style", "cursor: pointer; transition: opacity 0.3s; opacity: 0.6;"); // Geçiş efekti eklendi
    img.with = 25;
    img.height = 25;
    _renderer.appendChild(_element.nativeElement, img)
  }
  @Input() id!: string;
  @Input() controller?: string;
  @Input() action?: string;
  @Output() callback: EventEmitter<any> = new EventEmitter();

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
              this.callback.emit();
              this._alertify.message(
                "Product has been deleted successfully", {
                dismissOthers: true,
                messageType: MessageType.Success,
                position: Position.TopRight
              });
            },
            error: (errorResponse: HttpErrorResponse) => {
              this._alertify.message(
                "An error occurred while attempting to delete the product", {
                dismissOthers: true,
                messageType: MessageType.Error,
                position: Position.TopCenter
              });
              console.error(errorResponse);
              this.hideSpinner(SpinnerType.BallAtom);
            }
          })
      }

    });
    
  }

  @ViewChild('matButton', { static: true, read: ElementRef }) matButton!: ElementRef;

  openDialog(enterAnimationDuration: string, exitAnimationDuration: string, afterClosed: any): void {
    const dialogRef = this.dialog.open(DeleteDialogComponent,
      {
        width: '250px',
        enterAnimationDuration: enterAnimationDuration,
        exitAnimationDuration: exitAnimationDuration,
        data: DeleteState.Yes
      });

    dialogRef.afterClosed()
      .subscribe((result: DeleteState) => {
        if (result == DeleteState.Yes) {
          afterClosed();
        }
      })
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
