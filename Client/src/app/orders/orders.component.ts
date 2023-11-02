import { Component, OnInit } from '@angular/core';
import { OrdersService } from './orders.service';
import { take } from 'rxjs';
import { OrderDto } from '../dtos/responses/order.dto';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  orders: OrderDto[] = [];

  constructor(private ordersService: OrdersService) {

  }
  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders() {
    this.ordersService.getOrders().pipe(take(1)).subscribe(orders => this.orders = orders);
  }
}
