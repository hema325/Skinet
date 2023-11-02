import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { DeliveryMethodDto } from '../dtos/responses/delivery-method.dto';
import { PaymentIntentDto } from '../dtos/responses/payment-intent.dto';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  baseUrl = environment.baseUrl;

  constructor(private httpClient: HttpClient) { }

  getDeliveryMethods() {
    return this.httpClient.get<DeliveryMethodDto[]>(this.baseUrl + '/deliveryMethods');
  }

  createOrder(order: any) {
    return this.httpClient.post<number>(this.baseUrl + '/orders', order);
  }

  createPaymentIntent(obj: any) {
    return this.httpClient.post<PaymentIntentDto>(this.baseUrl + '/payments', obj);
  }
}
