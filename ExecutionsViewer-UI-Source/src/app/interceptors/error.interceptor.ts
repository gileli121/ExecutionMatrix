import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {ToastrService} from "ngx-toastr";
import {catchError} from "rxjs/operators";
import {ApiServiceService} from "../services/api-service.service";

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptor implements HttpInterceptor {

  private toastr: ToastrService;
  private apiService: ApiServiceService;


  constructor(toastr: ToastrService, apiService: ApiServiceService) {
    this.toastr = toastr;
    this.apiService = apiService;
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<unknown>> {


    return next.handle(req).pipe(
      catchError((error: any) => {
        if (error) {
          switch (error.status) {
            case 400:

              this.handle400Error(error);
              break;

            case 401:
              this.handle401Error(error);

              break;

            case 500:

              this.handle500Error(error);
              break;

            default:
              // Generic error handling
              this.handleUnExpectedError(error);
              break;
          }

          throw error;
        }

        return throwError("Unknown Error");
      })
    )
  }


  handle400Error(error: any) {
    if (!!error.error && Array.isArray(error.error)) {
      let errorMessage = '';
      for (const key in error.error) {
        if (!!error.error[key]) {
          const errorElement = error.error[key];
          errorMessage = (`${errorMessage}${errorElement.code} - ${errorElement.description}\n`)
        }
      }
      this.toastr.error(errorMessage, error.statusText);
      console.log(error.error);
    } else if (!!error?.error?.errors?.Content && (typeof error.error.errors.Content) === 'object') {
      let errorObject = error.error.errors.Content;
      let errorMessage = '';
      for (const key in errorObject) {
        const errorElement = errorObject[key];
        errorMessage = (`${errorMessage}${errorElement}\n`);
      }
      this.toastr.error(errorMessage, error.statusCode);
      console.log(error.error);
    } else if (!!error.error) {
      let errorMessage = ((typeof error.error === 'string')) ? error.error : 'There was a validation error.';
      this.toastr.error(errorMessage, error.statusCode);
      console.log(error.error);
    } else {
      this.toastr.error(error.statusText, error.status);
      console.log(error)
    }
  }


  handle401Error(error: any) {
    let errorMessage = 'Please login to your account.';
    // TODO: May add here logic to logout the account
    // TODO: Route to the login page
  }

  handle500Error(error: any) {
    this.toastr.error('Please contact the developer. An error happened in the server.');
    console.log(error);
  }

  handleUnExpectedError(error: any) {
    this.toastr.error('Please contact the developer. Something unexpected happened.')
    console.log(error);
  }
}
