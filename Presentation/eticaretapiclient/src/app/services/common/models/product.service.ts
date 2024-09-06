import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Create_Product } from '../../../contracts/create_product';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable, firstValueFrom } from 'rxjs';
import { List_Product } from '../../../contracts/list_product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  constructor(private httpClientService: HttpClientService) { }

  create(
    product: Create_Product,
    successCallBack?: () => void,
    errorCallBack?: (errorMessage: string) => void) {

    this.httpClientService.post({
      controller: "products",
      action: "CreateProduct"
    }, product)
      .subscribe({
        next: successCallBack,
        error: (errorResponse: HttpErrorResponse) => {
          const _error = errorResponse.error as Array<{ key: string, value: Array<string> }>;
          let message = "";
          if (_error && Array.isArray(_error)) {
            _error.forEach((_v) => {
              _v.value.forEach((v) => {
                message += `${v}<br>`;
              });
            });
          } else {
            message = errorResponse.message;
          }
          errorCallBack(message);
        }
      });
  }

  async read(page: number = 0, size: number = 5, successCallBack?: () => void, errorCallBack?: (errorMessage: string) => void): Promise<{ totalProductCount: number; products: List_Product[] }> {
    try {
      const promiseData = await firstValueFrom(
        this.httpClientService.get<{ totalProductCount: number; products: List_Product[] }>({
          controller: "products",
          action: "GetProductPaging",
          queryString: `page=${page}&size=${size}`
        })
      );
      if (successCallBack) {
        successCallBack();
      }
      return promiseData; 
    }
    catch (errorResponse: unknown) {
      if (errorResponse instanceof HttpErrorResponse) {
        if (errorCallBack) {
          errorCallBack(errorResponse.message);
        }
      }
      else {
        if (errorCallBack) {
          errorCallBack("An unexpected error occurred.");
        }
      }
      throw errorResponse;
    }
  }
    


  delete(id: string) {
    const deleteObservable: Observable<any> = this.httpClientService.delete<any>({
      controller: "products"
    }, id)
  }
}

