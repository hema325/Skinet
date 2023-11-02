import { Component, OnInit } from '@angular/core';
import { OrdersService } from '../orders.service';
import { ActivatedRoute } from '@angular/router';
import { OrderDto } from 'src/app/dtos/responses/order.dto';
import { take } from 'rxjs';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {

  order?: OrderDto;
  constructor(private ordersService: OrdersService,
    private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService) { }

  ngOnInit(): void {
    this.loadOrder();
    this.bcService.set('@order-header-title', ' ');
  }

  loadOrder() {
    const orderId = this.activatedRoute.snapshot.paramMap.get('id');
    if (!orderId) return;
    this.ordersService.getOrder(+orderId).pipe(take(1)).subscribe(order => {
      this.order = order;
      this.bcService.set('@order-header-title', `Order# ${order.id} - ${order.status}`);
    });
  }

}
