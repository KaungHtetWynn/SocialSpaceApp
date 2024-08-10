import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountService } from '../_services/account.service';

export const jwtbearerInterceptor: HttpInterceptorFn = (req, next) => {
  // As opposed to error interceptor, this time do it before we call next
  // Request is an immutable object, so we clone
  const accountService = inject(AccountService);

  const reqCloned = req.clone({
    setHeaders: {
      Authorization: `Bearer ${accountService.currentUser()?.token}`
    }
  });

  return next(reqCloned);
};
