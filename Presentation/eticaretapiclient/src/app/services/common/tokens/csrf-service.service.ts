import { Injectable } from '@angular/core';
import { Observable, catchError, map, throwError, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CsrfService {
  private csrfToken: string | null = null;  // CSRF token'ı depolayacağımız alan
  private csrfTokenObservable: Observable<string> | null = null;

  // `HttpClient` enjeksiyonu
  constructor(private httpClient: HttpClient) { }

  // CSRF token'ı almak için asenkron bir yöntem
  getCsrfToken(): Observable<string> {
    if (this.csrfToken) {
      return of(this.csrfToken); // Mevcut token'ı hemen döndür
    } else if (this.csrfTokenObservable) {
      return this.csrfTokenObservable;
    } else {
      // Token alınmamışsa, HTTP isteği yaparak token'ı alıyoruz
      this.csrfTokenObservable = this.httpClient.get<CsrfResponse>('https://localhost:7116/csrf-token', { withCredentials: true }).pipe(
        map(response => {
          console.log(response.csrfToken)
          if (response && response.csrfToken) {
            this.csrfToken = response.csrfToken;
            this.csrfTokenObservable = null;
            return this.csrfToken;
          } else {
            throw new Error('CSRF token alınamadı, yanıt boş.');
          }
        }),
        catchError(err => {
          console.error('CSRF token alınırken hata oluştu:', err);
          this.csrfTokenObservable = null;
          return throwError(() => new Error('CSRF token alınamadı'));
        })
      );
      return this.csrfTokenObservable;
    }
  }

  // `token` getter'ı: CSRF token'ı doğrudan almak için
  get token(): string | null {
    return this.csrfToken; // Bu getter CSRF token'ı döndürecek
  }
}

interface CsrfResponse {
  csrfToken: string;
}
