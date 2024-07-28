import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs';

// An interceptor for http requests made via HttpClient
// This method intercepts outgoing HTTP requests from client and incoming HTTP responses from the server.
// request: An instance of HttpRequest<unknown> representing the outgoing HTTP request.
// next: An instance of HttpHandler which is used to continue the request processing chain.
export const errorInterceptor: HttpInterceptorFn = (req, next) => {

  const router = inject(Router);
  const toastr = inject(ToastrService);

  // Returns observable so use pipe
  // RxJS catchError operator intercepts any errors that occur during the request or response processing.
  return next(req).pipe(
    
    // catching errors in server response and will display error noti
    catchError(err => {
      console.log(req)
      if (err) {
        switch (err.status) {
          case 400:
            if (err.error.errors) {
              const modalStateErrs = [];
              for (const key in err.error.errors) {
                if (err.error.errors[key]) {
                  modalStateErrs.push(err.error.errors[key])
                }
              }
              // this will pass down the chain and the error property in Observable will receive it
              console.log(modalStateErrs.flat())

              // This essentially injects the error back into the observable stream.
              // It will go to error property in register_error()
              // throwing will exit the switch
              throw modalStateErrs.flat();
            }
            else {
              toastr.error(err.error, err.status)
            }
            break;

          case 401:
            toastr.error('Unauthorised', err.status)
            break;

          case 404:
            router.navigateByUrl('/not-found')
            break;

          case 500:
            // The err is the HTTP error which contains an error object, 
            // and that error object contains an errors array for validation.
            const navigationExtras: NavigationExtras = { state: { error1: err.error } };
            router.navigateByUrl('/server-error', navigationExtras);
            break;

          default:
            toastr.error('Program encountered unexpected error')
            break;
        }
      }

      // the intercepted error is re-thrown to propagate it further in the application.
      throw err;
    })
  );
};
