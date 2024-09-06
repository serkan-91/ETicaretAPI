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

  //all_Products: ProductsWithTotalCount = { products: [], totalCount: 0 };
  displayedColumns: string[] = ['name', 'stock', 'price', 'createdDate', 'updatedDate'];
  dataSource: MatTableDataSource<List_Product> = null;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  async ngOnInit() {
    await this.GetProducts(); 
  }
  async GetProducts() {
    this.showSpinner(SpinnerType.BallAtom);
    const allProducts: { totalProductCount: number; products: List_Product[] } =
      await this.productService.read(
        this.paginator ? this.paginator.pageIndex : 0, this.paginator ? this.paginator.pageSize : 5, () =>
        this.hideSpinner(SpinnerType.BallAtom),
        errorMessage => this.alertifyService.message(errorMessage,
          {
      dismissOthers: true,
      messageType: MessageType.Error,
      position: Position.TopRight
          }))
    this.dataSource = new MatTableDataSource<List_Product>(allProducts.products);
    this.paginator.length = allProducts.totalProductCount;
  }

  async pageChanged() 
  {
    await this.GetProducts()
  } 


  //delete(id, event) {
  //  alert(id)
  //  const img: HTMLImageElement = event.srcElement;
  //  $(img.parentElement.parentElement.parentElement).fadeOut(2000)
  //}


   
  //async ngOnInit() {
  //      await this.GetProducts();
  //    }

  //onDeleteClick() {
  //      this.GetProducts();
  //    }
}


