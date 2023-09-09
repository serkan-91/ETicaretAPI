import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Create_Product } from '../../../contracts/create_product';
import { HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, firstValueFrom, lastValueFrom } from 'rxjs';
import { List_Product } from '../../../contracts/list_product';
import { ProductsWithTotalCount } from '../../../contracts/ProductsWithTotalCount';

@Injectable({
  providedIn: 'root'
})
export class ProductService {



  constructor(private httpClientService: HttpClientService) { }

  create(product: Create_Product, successCallBack?: any, errorCallBack?: (errorMessage: string) => void) {

    this.httpClientService.post({
      controller: "products"
    }, product)
      .subscribe(
        {
          next: successCallBack,
          error: (errorResponse: HttpErrorResponse) => {
            const _error: Array<{ key: string, value: Array<string> }> = errorResponse.error;
            let message = "";
            _error.forEach((v, index) => {
              _error.forEach((_v, _index) => {
                message += `${_v.value}<br>`;
              });
              errorCallBack(message);
            });

          }
        })


  }



  async list(page: number = 0, size: number = 5, successCallBack?: (data: ProductsWithTotalCount) => void, errorCallBack?: (errorMessage: string) => void): Promise<void> {
    try {
      const productData$ = this.httpClientService.get<ProductsWithTotalCount>({
        controller: "Products",
        queryString: `page=${page}&size=${size}`
      });

      const productList = await lastValueFrom(productData$);

      if (successCallBack) {
        successCallBack(productList);
      }

    }
    catch (error) {
      if (errorCallBack) {
        errorCallBack(error instanceof HttpErrorResponse ? error.message : 'An unknown error occurred');
      }

    }

  }

  delete(id: string) {
  const deleteObservable:  Observable<any> = this.httpClientService.delete<any>({
      controller: "products"
  }, id)
    var a = firstValueFrom(deleteObservable);
  }
}

