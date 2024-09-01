import { inject, Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusySpinnerService {
  busySpinnerRequestCount = 0;
  private spinnerService = inject(NgxSpinnerService);

  // control busy and idle behavior from interceptor
  busy() {
    this.busySpinnerRequestCount++;
    this.spinnerService.show(undefined, {
      type: 'ball-spin-clockwise',
      bdColor: 'rgba(255,255,255,0)',
      color: '#3b3b3b'
    })
  }

  idle() {
    this.busySpinnerRequestCount--;
    if(this.busySpinnerRequestCount <= 0) {
      this.busySpinnerRequestCount = 0;
      this.spinnerService.hide();
    }
  }

  constructor() { }
}
