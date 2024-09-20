import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CsrfService } from '../tokens/csrf-service.service';
import { switchMap } from 'rxjs/operators';

@Injectable()
export class CsrfInterceptorService implements HttpInterceptor {

  constructor(private csrfService: CsrfService) { }

  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // Yalnızca POST, PUT ve DELETE isteklerine CSRF token ekliyoruz
    if (req.method === 'POST' || req.method === 'PUT' || req.method === 'DELETE') {
      return this.csrfService.getCsrfToken().pipe(
        switchMap(csrfToken => {
          // Önce req.headers'ın mevcut olup olmadığını kontrol edin
          if (csrfToken) {
            const clonedRequest = req.clone({
              headers: req.headers.set('RequestVerificationToken', csrfToken)
            });
            return next.handle(clonedRequest); // Yeni isteği işleme al
          }
          // Eğer zaten token varsa veya token alınmamışsa orijinal isteği işleme al
          return next.handle(req);
        })
      );
    }

    // GET ve diğer istekler için token eklenmesine gerek yok
    return next.handle(req);
  }
}
