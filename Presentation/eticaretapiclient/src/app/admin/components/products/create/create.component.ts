import { Component, EventEmitter, Output } from '@angular/core';
import { ProductService } from '../../../../services/common/models/product.service';
import { Create_Product } from '../../../../contracts/create_product';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from '../../../../base/base.component';
import { AlertifyService, MessageType, Position } from '../../../../services/admin/alertify.service';
import { DialogParameters, FileUploadOptions } from '../../../../services/common/dialog.service';


@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent extends BaseComponent {

  constructor(
    spinner: NgxSpinnerService,
    private productService: ProductService,
    private alertify: AlertifyService) {

    super(spinner)

  }

  @Output() createdProduct: EventEmitter<Create_Product> = new EventEmitter();

  //disariya parametre gonderirken output kullanilir
  @Output() fileUploadOptions: Partial<FileUploadOptions> = {
    action: "upload",
    controller: "products",
    explanation: "Drag or choose pictures...",
    isAdminPage: true,
    accept: ".png, .jpg, .jpeg, .bmp"
  }

  create(name: HTMLInputElement, stock: HTMLInputElement, price: HTMLInputElement) {

    this.showSpinner
      ({
        spinnerNameType: SpinnerType.BallAtom

      });

    const create_product: Create_Product = new Create_Product();
    create_product.name = name.value;
    create_product.stock = parseInt(stock.value);
    create_product.price = parseFloat(price.value);



    this.productService.create(
      create_product,
      () => {
        this.hideSpinner(SpinnerType.BallAtom);

        this.alertify.message("This product has been successfully added", {
          dismissOthers: true,
          messageType: MessageType.Success,
          position: Position.TopRight
        });

        this.createdProduct.emit(create_product)
        this.hideSpinner(SpinnerType.BallAtom);

      },
      (errorMessage: string) => {
        this.hideSpinner(SpinnerType.BallAtom);

        this.alertify.message(errorMessage, {
          dismissOthers: true,
          messageType: MessageType.Error,
          position: Position.TopRight
        });
      }
    );
  }

}
