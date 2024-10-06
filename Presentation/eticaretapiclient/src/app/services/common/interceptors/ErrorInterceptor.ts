import {Injectable} from '@angular/core';
import {
    HttpErrorResponse,
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
    HttpResponse
} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {catchError, finalize} from 'rxjs/operators';
import {AlertifyService, Position} from '../../admin/alertify.service';
import {CustomToastrService, ToastrMessageType, ToastrPosition} from '../../ui/custom-toastr.service';
import {BaseComponent, SpinnerType} from '@app/base/base.component';
import {NgxSpinnerService} from 'ngx-spinner';

@Injectable()
export class ErrorInterceptor extends BaseComponent implements HttpInterceptor {
    constructor(
        private alertify: AlertifyService,
        private toaster: CustomToastrService,
        _spinner: NgxSpinnerService
    ) {
        super(_spinner);
    }

    intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        const skipSpinner: boolean  = req.headers.get('skipSpinner') === 'false';

        if (!skipSpinner) {
            this.showSpinner(SpinnerType.BallAtom);  // Eğer skipSpinner bayrağı yoksa spinner'ı göster
        }
        return next.handle ( req ).pipe (catchError ( (error : HttpErrorResponse) => {
            const isAdmin: boolean = req.headers.get ( 'isAdmin' ) === 'true';
            let errorMessage : string;
            if (error.error instanceof ErrorEvent) {
                errorMessage = `Client-side error: ${error.error.message}`;
            } else {
                // Server-side hata (API'den dönen hata)
                errorMessage = `Server-side error:` ;
                // API'den dönen detaylı hata mesajı
                if (error.error && error.error.message) {
                    errorMessage += ` - Details: ${error.error.message}`;
                }
            }
            if (isAdmin) {
                this.alertify.message ( `unexpectedError: ${errorMessage}`,
                    {
                        delay : 10,
                        messageType : 'error',
                        position : Position.TopCenter,
                        dismissOthers : true
                    } );
            } else {
                 this.toaster.message (
                     errorMessage,
                     'Hata oluştu',
                        {
                            messageType :ToastrMessageType.Error,
                            position : ToastrPosition.TopRight
                        } );

            }
            console.log ( errorMessage );
            return of ( new HttpResponse ( {body : null} ) );

        } ), finalize ( () => {
            if (!skipSpinner) {
                this.hideSpinner ( SpinnerType.BallAtom );  // Eğer skipSpinner bayrağı yoksa spinner'ı gizle
            }
        } ));
    }
}