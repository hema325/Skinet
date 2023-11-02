import { Component, OnInit, ViewChild } from '@angular/core';
import { BasketDetailsDto } from '../dtos/local/basket-details.dto';
import { BasketService } from '../basket/basket.service';
import { FormBuilder, Validators } from '@angular/forms';
import { numbers } from '../validators/numbers.validator';
import { DeliveryMethodDto } from '../dtos/responses/delivery-method.dto';
import { take } from 'rxjs';
import { CheckoutService } from './checkout.service';
import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';
import { CdkStepper } from '@angular/cdk/stepper';
import { PaymentIntentDto } from '../dtos/responses/payment-intent.dto';


@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit {

  basketDetails?: BasketDetailsDto;
  paymentIntent?: PaymentIntentDto;

  @ViewChild('stepper') stepper?: CdkStepper;

  orderForm = this.fb.group({
    addressForm: this.fb.group({
      street: ['', [Validators.required, Validators.maxLength(100)]],
      city: ['', [Validators.required, Validators.maxLength(100)]],
      state: ['', [Validators.required, Validators.maxLength(100)]],
      zipcode: ['', [Validators.required, Validators.maxLength(10), numbers()]],
    }),
    deliveryForm: this.fb.group({
      deliveryMethodId: ['', [Validators.required]]
    }),
    paymentForm: this.fb.group({
      nameOnCard: ['', [Validators.required]]
    })
  });

  shippingPrice: number = 0;
  subtotal: number = 0;

  constructor(private basketService: BasketService,
    private checkoutService: CheckoutService,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.loadProductDetails();
  }

  loadProductDetails() {
    this.basketService.getBasketDetails().pipe(take(1)).subscribe(detials => {
      this.basketDetails = detials;
      this.subtotal = detials.basketProducts.reduce((acc, pro) => acc + pro.price * pro.quantity, 0);
    });
  }

  updateCurrentDelivery(deliveryMethod: DeliveryMethodDto) {
    this.shippingPrice = deliveryMethod.price
  }



  createPaymentIntent() {
    const createPayment = {
      deliveryMethodId: this.orderForm.get('deliveryForm')?.value?.deliveryMethodId,
      items: this.basketDetails?.basketProducts.map(pro => {
        return {
          productId: pro.productId,
          quantity: pro.quantity
        }
      })
    };

    this.checkoutService.createPaymentIntent(createPayment).subscribe({
      next: result => {
        this.paymentIntent = result;
        this.stepper?.next();
      }
    });
  }
}
