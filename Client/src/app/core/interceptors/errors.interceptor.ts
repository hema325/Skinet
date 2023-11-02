import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorsInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(catchError(error => {

      if (error.status == 400) {
        if (error.error.errors.length)
          return throwError(() => error.error);

        this.toastr.error(error.error.message, error.status);
      }

      if (error.status == 401)
        this.router.navigateByUrl('/account/login');

      if (error.status == 404)
        this.router.navigateByUrl('/not-found');

      else if (error.status == 500) {
        const navigationExtras: NavigationExtras = { state: { error: error.error } };
        this.router.navigateByUrl('/server-error', navigationExtras);
      }

      return throwError(() => error.error);
    }));
  }
}
