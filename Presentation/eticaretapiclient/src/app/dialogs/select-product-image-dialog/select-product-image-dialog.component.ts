import { Component, Inject, OnInit } from '@angular/core';
import { BaseDialog } from '../base/base-dialogs';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FileUploadOptions } from '../../services/common/file-upload/file-upload.component';
import { ProductService } from '../../services/common/models/product.service';
import {   ProductImage   } from '../../contracts/list_product_image';
import { BehaviorSubject, Observable, delay  } from 'rxjs';
import { DialogService } from '../../services/common/dialog.service';
import { DeleteDialogComponent, DeleteState } from '../delete-dialog/delete-dialog.component';

@Component({
  selector: 'app-select-product-image-dialog',
  templateUrl: './select-product-image-dialog.component.html',
  styleUrls: ['./select-product-image-dialog.component.css'] // düzeltildi
})
export class SelectProductImageDialogComponent extends BaseDialog<SelectProductImageDialogComponent> implements OnInit {
  private imagesSubject: BehaviorSubject<ProductImage[]> = new BehaviorSubject<ProductImage[]>([]);
  images$: Observable<ProductImage[]> = this.imagesSubject.asObservable();
  images: ProductImage[] = [];
  constructor(  
    @Inject(MAT_DIALOG_DATA) public data: SelectProductImageState | string,
    _dialogRef: MatDialogRef<SelectProductImageDialogComponent>,
    private _productService: ProductService,
    private _dialogService: DialogService,
  ) {
    super(_dialogRef);
    this.imagesSubject.next(this.images);
  }


  options: Partial<FileUploadOptions> | undefined;
  

  ngOnInit(): void {
      this.options = {
      accept: ".png, .jpg, .jpeg, .gif",
      action: "UploadProductImage",
      controller: "products",
      explanation: "Ürün resimini seçin veya buraya sürükleyin...",
      isAdminPage: true,
      queryString: `${(this.data as any).id}`
    };
    
    this._productService.readImages((this.data as any).id)
      .pipe(delay(500))
      .subscribe((images: ProductImage[]) => {
        this.images = images;
        this.imagesSubject.next(images);
      });
  }
  
  deleteImage(imageId: string): void {
    this._dialogService.openDialog({
      componentType: DeleteDialogComponent,
      data: DeleteState.Yes,
      enterAnimationDuration: 500,
      exitAnimationDuration: 500,
      afterClosed: () => {
        this._productService.deleteImage((this.data as any).id, imageId)
          .pipe(delay(300))
          .subscribe({
            next: () => {
              const currentImages = this.imagesSubject.getValue();
              const updatedImages = currentImages.filter(image => image.id !== imageId);
              this.images = updatedImages;   
              this.imagesSubject.next(updatedImages);   
              }
            });
      }
    });
  }
  onFileUploaded(uploadedImages: ProductImage[]) {
    this.images = [...this.images, ...uploadedImages];
    this.imagesSubject.next(this.images);
}
}

export enum SelectProductImageState {
  Close ,  
}


