import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse
} from '@angular/common/http';
import { Observable, map, of, tap } from 'rxjs';

@Injectable()
export class CachingInterceptor implements HttpInterceptor {
  private cache = new Map<string, any>();
  constructor() { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (request.method != 'GET')
      return next.handle(request);

    const cachingKey = request.urlWithParams;
    const cachedResponse = this.cache.get(cachingKey);
    if (cachedResponse)
      return of(cachedResponse.clone());
    else {
      return next.handle(request).pipe(map(response => {
        this.cache.set(cachingKey, response);
        return response;
      }));
    }
  }
}
