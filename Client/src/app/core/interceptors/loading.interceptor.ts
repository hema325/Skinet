import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, delay, finalize, identity } from 'rxjs';
import { BusyService } from '../services/busy.service';
import { environment } from 'src/environments/environment.development';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyService: BusyService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    if (!request.url.includes('email'))
      this.busyService.setBusy();

    return next.handle(request).pipe(
      (environment.production ? identity : delay(1000)),
      finalize(() => this.busyService.setIdel()));
  }
}
