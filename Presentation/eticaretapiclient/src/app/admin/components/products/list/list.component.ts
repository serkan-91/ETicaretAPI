import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { List_Product } from '../../../../contracts/list_product';
import { ProductService } from '../../../../services/common/models/product.service';
import { BaseComponent, SpinnerType } from '../../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService, MessageType, Position } from '../../../../services/admin/alertify.service';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { ProductsWithTotalCount } from '../../../../contracts/ProductsWithTotalCount';
import { FaIconService } from '../../../../services/common/fa-Icon.service';

declare var $: any;

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css'],
})

export class ListComponent extends BaseComponent   {

  all_Products: ProductsWithTotalCount = { products: [], totalCount: 0 };
  dataSource = new MatTableDataSource<List_Product>(this.all_Products.products);
    
 

  constructor(
    spinner: NgxSpinnerService,
    private productService: ProductService,
    private alertifyService: AlertifyService,
    private faIconService : FaIconService
  )
  {
    super(spinner);
  }
  faXmark = this.faIconService.faXmark; 
  faPen = this.faIconService.faPen;

  totalCount: number;
 

  displayedColumns: string[] = ['name', 'stock', 'price', 'createdDate', 'updatedDate','edit','delete'];


  @ViewChild(MatPaginator) paginator: MatPaginator;
   

  async GetProducts() {
    try {
     
      this.showSpinner(SpinnerType.BallAtom);
       
      await this.productService.list(
        this.paginator ? this.paginator.pageIndex : 0,
        this.paginator ? this.paginator.pageSize : 5,
        (data: ProductsWithTotalCount) => {
          // Handle successful data retrieval, e.g., assign data to a component property
          this.all_Products = data;
          this.dataSource = new MatTableDataSource<List_Product>(this.all_Products.products);
          this.paginator.length = this.all_Products.totalCount;

        },
        (errorMessage: string) => {
          this.hideSpinner(SpinnerType.BallAtom);
          this.alertifyService.message(errorMessage, {
            dismissOthers: true,
            messageType: MessageType.Error,
            position: Position.TopRight,
          });
        }
      );
    } catch (error) {
      // Handle any unexpected errors here
      console.error('An error occurred:', error);
    }
  }


  //delete(id, event) {
  //  alert(id)
  //  const img: HTMLImageElement = event.srcElement;
  //  $(img.parentElement.parentElement.parentElement).fadeOut(2000)
  //}


  async pageChanged() {

    await this.GetProducts();
  }
  async ngOnInit() {
   await this.GetProducts();
  }
 
  onDeleteClick() {
    // GetProducts() işlemini burada çağırın
    this.GetProducts();
  }
}


