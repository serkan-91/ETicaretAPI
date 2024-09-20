import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { List_Product } from '../../../../contracts/list_product';
import { ProductService } from '../../../../services/common/models/product.service';
import { BaseComponent, SpinnerType } from '../../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService, MessageType, Position } from '../../../../services/admin/alertify.service';
import { MatPaginator } from '@angular/material/paginator';
import { FaIconService } from '../../../../services/common/fa-Icon.service';
import { DialogService } from '../../../../services/common/dialog.service';
import { SelectProductImageDialogComponent } from '../../../../dialogs/select-product-image-dialog/select-product-image-dialog.component';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css'],
})

export class ListComponent extends BaseComponent implements OnInit {

  displayedColumns: string[] = ['name', 'stock', 'price', 'createdDate', 'updatedDate', 'photos', 'edit', 'delete'];
  dataSource = new MatTableDataSource<List_Product>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    _spinner: NgxSpinnerService,
    private productService: ProductService,
    private alertifyService: AlertifyService,
    private faIconService: FaIconService,
    private _dialogService: DialogService
  ) {
    super(_spinner);
  }
 
  faXmark = this.faIconService.faXmark;
  faPen = this.faIconService.faPen;
  faImage = this.faIconService.faImage;

 
  GetProducts() {
    this.hideSpinner(SpinnerType.BallAtom)
    this.productService.read(
      this.paginator ? this.paginator.pageIndex : 0,
      this.paginator ? this.paginator.pageSize : 5,
      () => this.hideSpinner(SpinnerType.BallAtom),
      errorMessage => this.alertifyService.message(errorMessage, {
        dismissOthers: true,
        messageType: MessageType.Error,
        position: Position.TopRight
      })
    ).subscribe({
      next: (allProducts: { totalCount: number; items: List_Product[] }) => {
        this.dataSource = new MatTableDataSource<List_Product>(allProducts.items);
        this.paginator.length = allProducts.totalCount;
      },
      error: (error) => {
        console.error(error);
      }
    });
  }

  addProductImageDialog(id: string) {
    this._dialogService.openDialog({
      componentType: SelectProductImageDialogComponent,
      enterAnimationDuration: 600,
      exitAnimationDuration: 600,
      data: id,
      options: {
        minWidth: '50vw'
      }
    });
  }

  delete(event: { srcElement: HTMLImageElement; }) {
    const parent = event.srcElement.parentElement?.parentElement?.parentElement;


    if (parent) {
      $(parent).fadeOut(2000);
    }
  }

  onDeleteClick() {
    this.GetProducts();
  }

  pageChanged() {
    this.GetProducts();
  }
  async ngOnInit() {
    this.GetProducts();
  }
}
