import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpErrorResponse, HttpEvent, HttpResponse } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { AlertifyService, Position } from '../../admin/alertify.service';
import { ToastrService } from 'ngx-toastr';
import { ToastrPosition } from '../../ui/custom-toastr.service';
import { BaseComponent, SpinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable()
export class ErrorInterceptor extends BaseComponent implements HttpInterceptor {
  constructor(
    private alertify: AlertifyService,
    private toaster: ToastrService,
    _spinner: NgxSpinnerService
  ) {
    super(_spinner);
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const skipSpinner = req.headers.get('skipSpinner') === 'false';  // Spinner'ı atlamak için bayrak kontrolü

    if (!skipSpinner) {
      this.showSpinner(SpinnerType.BallAtom);  // Eğer skipSpinner bayrağı yoksa spinner'ı göster
    }
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        const isAdmin = req.headers.get('isAdmin') === 'true';
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
          // Client-side hata (örneğin, ağ bağlantısı hatası)
          errorMessage = `Client-side error: ${error.error.message}`;
        } else {
          // Server-side hata (API'den dönen hata)
          errorMessage = `Server-side error: ${error.status} ${error.message}`;

          // API'den dönen detaylı hata mesajı
          if (error.error && error.error.message) {
            errorMessage += ` - Details: ${error.error.message}`;
          }
        }
        if (isAdmin) {
          // Admin için hata mesajı
          this.alertify.message(`unexpectedError: ${errorMessage}`, {
            delay: 10,
            messageType: 'error',
            position: Position.TopCenter,
            dismissOthers: true
          });
        } else {
          // Kullanıcı için hata mesajı
          this.toaster.error(`unexpectedError: ${errorMessage}`, 'Error', {
            timeOut: 10,
            positionClass: ToastrPosition.TopCenter
          });
        } 
        // Hata fırlatmak yerine, boş bir observable döndürülüyor
        return of(new HttpResponse({ body: null }));

      }), finalize(() => {
        if (!skipSpinner) {
          this.hideSpinner(SpinnerType.BallAtom);  // Eğer skipSpinner bayrağı yoksa spinner'ı gizle
        }
      })
    );
  }
}
