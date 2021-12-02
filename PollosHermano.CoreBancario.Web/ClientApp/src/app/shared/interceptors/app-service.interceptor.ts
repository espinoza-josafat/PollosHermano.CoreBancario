import {
    HttpEvent, HttpHandler,

    HttpInterceptor, HttpRequest
} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { NgxSpinnerService } from "ngx-spinner";
import { Observable, throwError } from "rxjs";
import { catchError, finalize } from "rxjs/operators";
import { AuthService } from "../auth/auth.service";
import { LocalStorageService } from "../storage/local-storage.service";


@Injectable()
export class AppServiceInterceptor implements HttpInterceptor {
  private countRequest: number = 0;

  constructor(
    private localStorageService: LocalStorageService,
    private spinner: NgxSpinnerService,
    private authService: AuthService,
  ) {

  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (!this.countRequest) {
      this.spinner.show(undefined, {
        type: "ball-triangle-path",
        size: "medium",
        bdColor: "rgba(0, 0, 0, 0.8)",
        color: "#fff",
        fullScreen: true
      });
    }

    this.countRequest++;

    let token = this.localStorageService.get("token");

    token = token ? token : "";

    request = request.clone({
      url: request.url,
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
    return next.handle(request).pipe(
      finalize(() => {
        this.countRequest--;
        if (!this.countRequest) {
          this.spinner.hide();
        }
      })/*,
      catchError((error: any) => {
        try {
          if ([401, 403].indexOf(error.status) !== -1) {
            // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
            this.authService.logout();
          }
        }
        catch (err) {

        }

        return throwError(error);
      })*/
    );
  }
}
