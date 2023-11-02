import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Stripe, StripeCardCvcElement, StripeCardExpiryElement, StripeCardNumberElement, loadStripe } from '@stripe/stripe-js';
import { BasketDetailsDto } from 'src/app/dtos/local/basket-details.dto';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { PaymentIntentDto } from 'src/app/dtos/responses/payment-intent.dto';
import { BusyService } from 'src/app/core/services/busy.service';
import { firstValueFrom } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.css']
})
export class CheckoutPaymentComponent implements OnInit {

  @Input() orderForm?: FormGroup;
  @Input() basketDetails?: BasketDetailsDto;
  @Input() paymentIntent?: PaymentIntentDto;

  @ViewChild('cardNumber') cardNumberElement?: ElementRef;
  @ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
  @ViewChild('cardCvc') cardCvcElement?: ElementRef;

  stripe: Stripe | null = null;
  stripeCardNumber?: StripeCardNumberElement;
  stripeCardExpiry?: StripeCardExpiryElement;
  stripeCardCvc?: StripeCardCvcElement;

  cardNumberError?: string;
  cardExpiryError?: string;

  cardNumberCompleted: boolean = false;
  cardExpiryCompleted: boolean = false;
  cardCvcCompleted: boolean = false;

  constructor(private checkoutService: CheckoutService,
    private toastrService: ToastrService,
    private router: Router,
    private basketService: BasketService,
    private busyService: BusyService) { }

  get paymentForm(): any {
    return (this.orderForm?.controls['paymentForm']);
  }

  get paymentFormValid(): boolean {
    return this.orderForm?.invalid ||
      !this.cardNumberCompleted ||
      !this.cardExpiryCompleted ||
      !this.cardCvcCompleted;
  }

  ngOnInit(): void {
    loadStripe(environment.publishableKey)
      .then(stripe => {
        this.stripe = stripe;
        const elements = stripe?.elements();

        if (elements) {
          this.stripeCardNumber = elements.create('cardNumber');
          this.stripeCardNumber.mount(this.cardNumberElement?.nativeElement);
          this.stripeCardNumber.on('change', e => {
            this.cardNumberError = e.error?.message;
            this.cardNumberCompleted = e.complete;
          });

          this.stripeCardExpiry = elements.create('cardExpiry');
          this.stripeCardExpiry.mount(this.cardExpiryElement?.nativeElement);
          this.stripeCardExpiry.on('change', e => {
            this.cardExpiryError = e.error?.message;
            this.cardExpiryCompleted = e.complete;
          });

          this.stripeCardCvc = elements.create('cardCvc');
          this.stripeCardCvc.mount(this.cardCvcElement?.nativeElement);
          this.stripeCardCvc.on('change', e => {
            this.cardCvcCompleted = e.complete;
          });
        }
      });
  }

  async onSubmit() {

    const order = this.getOrderFromForm();
    this.busyService.setBusy();
    try {
      await this.cofirmCardPayment()
      const orderId = await this.createOrder();

      this.basketService?.clearBasket();
      this.toastrService.success('Order is created successfully');

      this.router.navigateByUrl('/checkout/success', { state: { orderId } });
    }
    catch (error: any) {
      this.toastrService.error(error.message);
    }
    finally {
      this.busyService.setIdel();
    }
  }

  private async cofirmCardPayment() {
    if (!this.paymentIntent)
      throw new Error('No payment intent');

    const result = await this.stripe?.confirmCardPayment(this.paymentIntent.clientSecret, {
      payment_method: {
        card: this.stripeCardNumber!,
        billing_details: {
          name: this.orderForm?.get('nameOnCard')?.value
        }
      }
    })

    if (!result?.paymentIntent)
      throw new Error(result?.error.message);

  }

  private createOrder() {
    const order = this.getOrderFromForm();
    return firstValueFrom(this.checkoutService.createOrder(order));
  }

  private getOrderFromForm() {
    return {
      shippingAddress: this.orderForm?.get('addressForm')?.value,
      deliveryMethodId: this.orderForm?.get('deliveryForm')?.value?.deliveryMethodId,
      items: this.basketDetails?.basketProducts.map(pro => {
        return {
          productId: pro.productId,
          quantity: pro.quantity
        }
      })
    }
  }
}
