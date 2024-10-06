import { Component, EventEmitter, Output, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { PagingResult, Product } from '@app/contracts/list_product';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { FaIconService } from '@app/services/common/fa-Icon.service';
import { IconDefinition } from '@fortawesome/angular-fontawesome';
import { MatSort } from '@angular/material/sort';
import { DialogParameters, DialogService } from '@app/services/common/dialog.service';
import { SelectProductImageDialogComponent } from '@app/dialogs/select-product-image-dialog/select-product-image-dialog.component';
import { BaseComponent } from '@app/base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ProductService } from '@app/services/common/models/product.service';
import { FileUploadOptions } from '@app/services/common/file-upload/file-upload.component';


@Component({
  selector: 'app-list',
  templateUrl: './list.component.html'
})
export class ListComponent extends BaseComponent implements OnInit, AfterViewInit {
  fileUploadOptions: Partial<FileUploadOptions> = {
    action: 'UploadProductImage',
    controller: 'products',
    explanation: 'The pictures drag and drop or choose',
    isAdminPage: true,
    accept: '.png, .jpg, .jpeg, .pdf, .mp4'
  }
  @Output() pageChange = new EventEmitter<PageEvent>(); // Emit page change events
  displayedColumns: string[] = ['name', 'stock', 'price', 'createdDate', 'updatedDate', 'photos', 'edit', 'delete'];
    ProductModel?: (PagingResult<Product>) 
  dataSource: MatTableDataSource<Product> = new MatTableDataSource<Product>();
  isLoadingResults = true; // Veriler yüklenirken true olacak
  isRateLimitReached = false; // Rate limit hatası alınırsa true olacak
  totalCount = 0;  // Toplam veri sayısı
  pageSize = 5;    


  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  faXmark!: IconDefinition;
  faPen!: IconDefinition;
  faImage!: IconDefinition;
  @Output() imageDialogEvent = new EventEmitter<string>();  // string parametre ile EventEmitter oluşturuyoruz
    
  constructor(
    private faIconService: FaIconService,
    private dialogService: DialogService,
    private productService: ProductService,
    _spinner: NgxSpinnerService) {
    super(_spinner);
  }
  ngOnInit(): void {
    this.loadProducts(0, this.pageSize);
  }

  ngAfterViewInit() {
    this.faXmark = this.faIconService.faXmark;
    this.faPen = this.faIconService.faPen;
    this.faImage = this.faIconService.faImage;

    this.paginator.page.subscribe(() => {
      this.loadProducts(this.paginator.pageIndex, this.paginator.pageSize);
    });

    this.sort.sortChange.subscribe(() => {
      this.loadProducts(this.paginator.pageIndex, this.paginator.pageSize);
    });
  }

  trackById(item: Product) {
    return item.id;
  }


  productImagesDialog(id: string) {
    const imageDialogParams: Partial<DialogParameters> = {
      componentType: SelectProductImageDialogComponent,
      data: { id },
      enterAnimationDuration: 500,
      exitAnimationDuration: 500,
      options: {
        minWidth: '1200px'
      }
    }
    this.dialogService.openDialog(imageDialogParams);
  }

  addProduct(newProduct: Product) {
    const currentData = this.dataSource.data;
    currentData.push(newProduct);
    this.dataSource.data = currentData;
    this.totalCount += 1;
    this.paginator.length = this.totalCount;
    if (this.totalCount > this.paginator.pageSize * (this.paginator.pageIndex + 1)) {
      // Eğer toplam veri sayısı mevcut sayfa boyutunu aşıyorsa, bir sonraki sayfaya geçiyoruz
      this.paginator.nextPage();
    }
    // Paginator'da sayfa değişiklikleri olduğunda ürünleri yeniden yükleyelim
    this.paginator.page.subscribe(() => {
      this.loadProducts(this.paginator.pageIndex, this.paginator.pageSize);
    });
  }
   
  onItemDeleted(deletedItemId: string) {
    // setTimeout fonksiyonunu kullanarak işlemi 500ms geciktiriyoruz
    setTimeout(() => {
      const deletedData = this.dataSource.data.filter(item => item.id !== deletedItemId);
      this.dataSource.data = deletedData;
      this.totalCount -= 1;
      this.paginator.length = this.totalCount;

      // Eğer sayfadaki tüm ürünler silinmişse, önceki sayfaya dönüyoruz
      if (deletedData.length === 0 && this.paginator.hasPreviousPage()) {
        this.paginator.previousPage();
      } else {
        // Mevcut sayfadaki ürünleri yeniden yüklüyoruz
        this.loadProducts(this.paginator.pageIndex, this.paginator.pageSize);
      }

      // paginator.page olayını dinleyerek sayfa değişikliklerinde ürünleri yeniden yüklüyoruz
      this.paginator.page.subscribe(() => {
        this.loadProducts(this.paginator.pageIndex, this.paginator.pageSize);
      });
    }, 500);  // 500ms gecikme
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  loadProducts(pageIndex: number, pageSize: number): void {
    this.isLoadingResults = true;
    this.productService.read(pageIndex, pageSize, false).subscribe(response => {
      this.dataSource.data = response.pagingResult.items;
      this.totalCount = response.pagingResult.totalCount;
      // this.sort.active,
      //  this.sort.direction,
       this.paginator.length = this.totalCount;
      this.isLoadingResults = false;
    });
  }
}

