import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { OrderDto } from '../dtos/responses/order.dto';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  baseUrl = environment.baseUrl;
  constructor(private httpClient: HttpClient) { }

  getOrders() {
    return this.httpClient.get<OrderDto[]>(this.baseUrl + '/orders/currentUserOrders');
  }

  getOrder(orderId: number) {
    return this.httpClient.get<OrderDto>(this.baseUrl + '/orders/' + orderId);
  }
}
