/* eslint-disable no-unused-vars */
import { Component, Inject, OnInit, Output } from '@angular/core';
import { BaseDialog } from '../base/base-dialogs';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FileUploadOptions } from '../../services/common/file-upload/file-upload.component';
import { ProductService } from '../../services/common/models/product.service';
import { List_Product_Image } from '../../contracts/list_product_image';
import { Observable, delay, map, of } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from '../../base/base.component';
import { DialogService } from '../../services/common/dialog.service';
import { DeleteDialogComponent, DeleteState } from '../delete-dialog/delete-dialog.component';

@Component({
  selector: 'app-select-product-image-dialog',
  templateUrl: './select-product-image-dialog.component.html',
  styleUrl: './select-product-image-dialog.component.css'
})
export class SelectProductImageDialogComponent extends BaseDialog<SelectProductImageDialogComponent> implements OnInit {
  constructor(
    _dialogRef: MatDialogRef<SelectProductImageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SelectProductImageState | string,
    private _productService: ProductService,
    private _spinner: NgxSpinnerService,
    private _dialogService: DialogService
  ) {
    super(_dialogRef);
  }

  @Output() options: Partial<FileUploadOptions> = {
    accept: ".png, .jpg, .jpeg, .gif, .bmp",
    action: "UploadProductImage",
    controller: "Products",
    explanation: "Choose a product image or drag here a product image",
    isAdminPage: true,
    queryString: `id=${this.data}`
  }
  images$: Observable<List_Product_Image[]> = of([]);;

  ngOnInit(): void {
    this.getImages();
  }

  getImages(data: string = this.data as string) {
    this._spinner.show(SpinnerType.BallAtom)
    this.images$ = this._productService.readImages(data)
      .pipe(delay(500));

    this.images$.subscribe({
      next: () => {
        this._spinner.hide(SpinnerType.BallAtom); // Gecikmeden sonra spinner kapanır
      },
      error: (error) => {
        this._spinner.hide(SpinnerType.BallAtom); // Hata durumunda da spinner kapanır
        console.error('Error:', error);
      }
    });
  }

  deleteImage(imageId: string): void {
    this._dialogService.openDialog({
      componentType: DeleteDialogComponent,
      data: DeleteState.Yes,
      enterAnimationDuration: 500,
      exitAnimationDuration: 500,
      afterClosed: () => {
        this._spinner.show(SpinnerType.BallAtom)
        this._productService.deleteImage(this.data as string, imageId)
          .pipe(
            delay(400)
          )
          .subscribe({
            next: () => {
              if (this.images$) {
                this.images$ = this.images$.pipe(
                  map(images => images.filter(image => image.id !== imageId))
                );
              }
              this._spinner.hide(SpinnerType.BallAtom);
            },
            error: (err) => {
              this._spinner.hide(SpinnerType.BallAtom);
              console.error('Error occured while attended deleting product images!', err);
            }
          })
      }
    })
  }
}

export enum SelectProductImageState {
  Close = 0,
}
