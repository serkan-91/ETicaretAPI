import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Create_Product } from '../../../contracts/create_product';
import { Product, ProductsResponse } from '../../../contracts/list_product';
import { BaseComponent } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { ProductImage } from '../../../contracts/list_product_image';
import { HttpHeaders, HttpParams } from '@angular/common/http';
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
 

  read(pageNumber: number = 0, pageSize: number = 5, isSpinnerActive: boolean = true ): Observable<ProductsResponse> {
    const queryString = `pageNumber=${pageNumber}&pageSize=${pageSize}`; 
    const headers = new HttpHeaders({
      'isAdmin': 'true',
      'skipSpinner': String(!isSpinnerActive)
    });
    return this.httpClientService.get<ProductsResponse>({
      controller: 'products',
      action: 'GetProductsPaging',
      queryString: queryString,
      headers: headers
    });  
  }
  create(product: Create_Product): Observable<Product> {
    return this.httpClientService.post<Create_Product, Product>(
      {
        controller: 'products',
        action: 'CreateProduct'
      },
      product
    );
  }


  delete({ id }: { id: string; successCallBack?: () => void; errorCallBack?: () => void; }): void {
    this.httpClientService.delete<unknown>({
      controller: 'products',
      action: 'DeleteProduct'
    }, id);
  }
  deleteImage(id: string, imageId: string)  {
  return  this.httpClientService.delete({
    controller: 'products',
      action: 'DeleteProductImage',
      queryString: `imageId=${imageId}`
    },id)
  }

  readImages(id: string): Observable<ProductImage[]> {
    return this.httpClientService.get<ProductImage[]>({
      controller: 'products',
      action: 'GetProductImages'
    }, id);
  }
}
