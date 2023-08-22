import { HttpBackend, HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface BackendParam {
  name: string;
  value: string;
}

export interface BackendError {
  errorKey: string;
  errorMessage: string;
}

export interface BackendResponse<T> {
  success: boolean;
  errors: BackendError[];
  data: T;
}

@Injectable({
  providedIn: 'root',
})
export class BackendService {
  httpRaw: HttpClient;

  constructor(
    private http: HttpClient,
    rawHandler: HttpBackend
  ) {
    this.httpRaw = new HttpClient(rawHandler);
  }

  private getHttpClient(disableInterceptors: boolean): HttpClient {
    return disableInterceptors ? this.httpRaw : this.http;
  }

  public parseParams(params: BackendParam[]): HttpParams {
    let httpParams: HttpParams = new HttpParams();

    params.forEach(x => {
      httpParams = httpParams.append(x.name, x.value);
    });

    return httpParams;
  }

  private addHeaders(): HttpHeaders {
    const headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    return headers;
  }

  public get<T>(
    url: string,
    params: BackendParam[] = [],
    disableInterceptors = false
  ): Observable<BackendResponse<T>> {
    const queryString = this.parseParams(params);
    const headers = this.addHeaders();
    return this.getHttpClient(disableInterceptors).get<BackendResponse<T>>(
      url,
      {
        headers,
        params: queryString
      }
    );
  }

  public getCookie<T>(
    url: string,
    params: BackendParam[] = [],
    disableInterceptors = false
  ): Observable<BackendResponse<T>> {
    const queryString = this.parseParams(params);
    const headers = this.addHeaders();
    return this.getHttpClient(disableInterceptors).get<BackendResponse<T>>(
      url,
      {
        headers,
        params: queryString,
        withCredentials: false
      }
    );
  }

  public getFile(
    url: string,
    params: BackendParam[] = [],
    disableInterceptors = false
  ): Observable<any> {
    const httpClient = this.getHttpClient(disableInterceptors);
    const headers = this.addHeaders();
    const queryString = this.parseParams(params);

    return httpClient.get(url, { headers, params: queryString, responseType: 'arraybuffer' });
  }

  public post<T>(
    url: string,
    data: any,
    disableInterceptors = false
  ): Observable<BackendResponse<T>> {
    const headers = this.addHeaders();
    return this.getHttpClient(disableInterceptors).post<BackendResponse<T>>(
      url,
      data,
      {
        headers
      }
    );
  }

  public put<T>(
    url: string,
    data: any,
    disableInterceptors = false
  ): Observable<BackendResponse<T>> {
    const headers = this.addHeaders();
    return this.getHttpClient(disableInterceptors).put<BackendResponse<T>>(
      url,
      data,
      {
        headers
      }
    );
  }

  public delete<T>(
    url: string,
    params: BackendParam[] = [],
    disableInterceptors = false
  ): Observable<BackendResponse<T>> {
    const parsedParams = this.parseParams(params);
    const headers = this.addHeaders();

    return this.getHttpClient(disableInterceptors).delete<
      BackendResponse<T>
    >(url, {
      headers,
      params: parsedParams
    });
  }
}
