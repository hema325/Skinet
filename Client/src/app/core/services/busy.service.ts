import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {

  private busyRequestCount = 0;
  constructor(private spinnerService: NgxSpinnerService) { }

  setBusy() {
    ++this.busyRequestCount;
    this.spinnerService.show();
  }

  setIdel() {
    --this.busyRequestCount;
    if (this.busyRequestCount <= 0) {
      this.spinnerService.hide();
      this.busyRequestCount = 0;
    }
  }
}
