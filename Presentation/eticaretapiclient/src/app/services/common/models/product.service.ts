import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Create_Product } from '../../../contracts/create_product';
import { HttpErrorResponse } from '@angular/common/http';
import { List_Product, Pagination } from '../../../contracts/list_product';
import { BaseComponent } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { List_Product_Image } from '../../../contracts/list_product_image';
@Injectable({
  providedIn: 'root'
})
export class ProductService extends BaseComponent {
  constructor(
    _spinner: NgxSpinnerService,
    private httpClientService: HttpClientService
  ) {
    super(_spinner);
  }

  create(
    product: Create_Product,
    successCallBack?: () => void,
    errorCallBack?: (errorMessage: string) => void
  ) {
    this.httpClientService.post({
      controller: "products",
      action: "CreateProduct"
    }, product)
      .subscribe({
        next: () => {
          if (successCallBack) {
            successCallBack();
          }
        },
        error: (error) => {
          const _error = error.error as Array<{ key: string, value: Array<string> }>;
          let message = "";
          if (_error && Array.isArray(_error)) {
            _error.forEach((_v) => {
              _v.value.forEach((v) => {
                message += `${v}<br>`;
              });
            });
          } else {
            message = error.message;
          }
          if (errorCallBack) {
            errorCallBack(message);
          }
        }
      });
  }

  read(pagination: Partial<Pagination>, successCallBack?: () => void, errorCallBack?: (errorMessage: string) => void): Observable<{ totalCount: number; items: List_Product[] }> {
    const observableData: Observable<{ totalCount: number; items: List_Product[] }> =
      this.httpClientService.get<{ totalCount: number; items: List_Product[] }>({
      controller: "products",
      action: "GetProductsPaging",
      queryString: `page=${pagination.page ?? 0}&size=${pagination.size ?? 5}`  // Eğer page veya size undefined ise varsayılan 0 ve 5 kullanılıyor
    });

    // Subscribe işlemi ile veriyi yakalıyoruz
    observableData.subscribe({
      next: () => {
        // Başarılı geri dönüş aldığında callback fonksiyonunu çalıştır
        if (successCallBack) {
          successCallBack(); // Eğer bir veri işlemek istiyorsanız, burada data'yı da kullanabilirsiniz
        }
      },
      error: (errorResponse: HttpErrorResponse) => {
        // Hata durumunda errorCallBack fonksiyonunu çalıştır
        if (errorCallBack) {
          errorCallBack(errorResponse.message);
        }
      }
    });

    // Observable döndürüyoruz, böylece başka bileşenler de bu veriyi dinleyebilir
    return observableData;
  }

  delete({ id }: { id: string; successCallBack?: () => void; errorCallBack?: () => void; }): void {
    this.httpClientService.delete<any>({
      controller: "products",
      action: "DeleteProduct"
    }, id);
  }
  deleteImage(id: string, imageId: string)  {
  return  this.httpClientService.delete({
      controller: "products",
      action: "DeleteProductImage",
      queryString: `imageId=${imageId}`
    },id)
  }

  readImages(id: string): Observable<List_Product_Image[]> {
    return this.httpClientService.get<List_Product_Image[]>({
      controller: "products",
      action: "GetProductImages"
    }, id);
  }
}
