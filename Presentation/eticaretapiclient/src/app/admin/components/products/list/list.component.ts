import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { List_Product } from '../../../../contracts/list_product';
import { ProductService } from '../../../../services/common/models/product.service';
import { BaseComponent, SpinnerType } from '../../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService, MessageType, Position } from '../../../../services/admin/alertify.service';
import { MatPaginator } from '@angular/material/paginator';
import { FaIconService } from '../../../../services/common/fa-Icon.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css'],
})

export class ListComponent extends BaseComponent implements OnInit {
  constructor(
    spinner: NgxSpinnerService,
    private productService: ProductService,
    private alertifyService: AlertifyService,
    private faIconService: FaIconService
  ) {
    super(spinner);
  }
  faXmark = this.faIconService.faXmark;
  faPen = this.faIconService.faPen;

  displayedColumns: string[] = ['name', 'stock', 'price', 'createdDate', 'updatedDate', 'edit', 'delete'];
  dataSource: MatTableDataSource<List_Product> = null;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  async ngOnInit() {
    this.GetProducts();
  }
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
      next: (allProducts: { totalProductCount: number; products: List_Product[] }) => {
        this.dataSource = new MatTableDataSource<List_Product>(allProducts.products);
        this.paginator.length = allProducts.totalProductCount;
      },
      error: (error) => {
        console.error(error);
      }
    });
  }

  delete(event: { srcElement: HTMLImageElement; }) {
    $(event.srcElement.parentElement.parentElement.parentElement).fadeOut(2000)
  }

  onDeleteClick() {
    this.GetProducts();
  }

  pageChanged() {
    this.GetProducts();
  }
}
