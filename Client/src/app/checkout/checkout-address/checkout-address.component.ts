import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.css']
})
export class CheckoutAddressComponent implements OnInit {

  @Input() addressForm!: any;

  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private toatrService: ToastrService) { }


  ngOnInit() {
    this.accountService.loadCurrentAuth().pipe(take(1)).subscribe(response => this.addressForm.setValue(response!.address));
  }

  updateUserAddress() {
    this.accountService.updateUserAddress(this.addressForm.value).pipe(take(1)).subscribe({
      complete: () => {
        this.toatrService.success('Address has been saved successfully');
        this.addressForm.reset(this.addressForm.value);
      }
    });
  }



}

