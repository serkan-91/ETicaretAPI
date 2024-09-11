import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpClientService {
  constructor(private httpClient: HttpClient, @Inject("baseUrl") private baseUrl: string) { }

  private url(requestParameter: Partial<RequestParameters>): string {
    const baseUrl = requestParameter.baseUrl ? requestParameter.baseUrl : this.baseUrl;
    const controllerUrl = `/${requestParameter.controller}`;
    const actionUrl = requestParameter.action ? `/${requestParameter.action}` : "";
    return `${baseUrl}${controllerUrl}${actionUrl}`;
  }

  private buildUrl(requestParameter: Partial<RequestParameters>, id?: string): string {
    let url = requestParameter.fullEndPoint ? requestParameter.fullEndPoint : this.url(requestParameter);
    // Append the ID if provided
    if (id) {
      url += `/${id}`;
    }
    // Append the query string if present
    if (requestParameter.queryString) {
      url += `?${requestParameter.queryString}`;
    }
    return url;
  }

  get<T>(requestParameter: Partial<RequestParameters>, id?: string): Observable<T> {
    const url = this.buildUrl(requestParameter, id);
    const headers = requestParameter.headers ? requestParameter.headers : new HttpHeaders();
    return this.httpClient.get<T>(url, { headers });
  }

  post<T>(requestParameter: Partial<RequestParameters>, body: Partial<T>): Observable<T> {
    const url = this.buildUrl(requestParameter);
    const headers = requestParameter.headers ? requestParameter.headers : new HttpHeaders();

    return this.httpClient.post<T>(url, body, { headers });
  }

  put<T>(requestParameter: Partial<RequestParameters>, body: Partial<T>): Observable<T> {
    const url = this.buildUrl(requestParameter);
    const headers = requestParameter.headers ? requestParameter.headers : new HttpHeaders();
    return this.httpClient.put<T>(url, body, { headers });
  }

  delete<T>(requestParameter: Partial<RequestParameters>, id: string): Observable<T> {
    const url = this.buildUrl(requestParameter, id);
    const headers = requestParameter.headers ? requestParameter.headers : new HttpHeaders();
    return this.httpClient.delete<T>(url, { headers });
  }
}

export class RequestParameters {
  controller?: string;
  action?: string;
  queryString?: string;
  headers?: HttpHeaders;
  baseUrl?: string;
  fullEndPoint?: string;
}
