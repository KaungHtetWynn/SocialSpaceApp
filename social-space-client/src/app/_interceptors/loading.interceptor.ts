import { HttpInterceptorFn } from '@angular/common/http';
import { BusySpinnerService } from '../_services/busy-spinner.service';
import { inject } from '@angular/core';
import { delay, finalize } from 'rxjs';

// Interceptor intercepts the request, modify it and then let it go to its destination
export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busyService = inject(BusySpinnerService);

  // request comes in
  // Starts busy spinner before request is about go out to api
  busyService.busy();

  // When we get the response back from api, stop loading spinner
  // You can configure the request and then pass the request to next
  return next(req).pipe(
    delay(1000),
    finalize(() => {
      busyService.idle()
    }) // request is completed and comes back from api then use finalize to set spinner to idle
  );
};
