import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

// This route guard is a js/ts function not a class
export const authGuard: CanActivateFn = (route, state) => {

  const accountService: AccountService = inject(AccountService);
  const toastrService: ToastrService = inject(ToastrService);

  if(accountService.currentUser()) {
    return true;
  }
  else {
    toastrService.error("Please log in to perform this action!");
    return false;
  }

};
