import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let token: string | undefined;
    this.accountService.currentAuth$.pipe(take(1)).subscribe(auth => token = auth?.token)

    if (token)
      request = request.clone({
        setHeaders: {
          authorization: `Bearer ${token}`
        }
      });

    return next.handle(request);
  }
}
