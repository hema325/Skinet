import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { OrdersComponent } from './orders.component';
import { OrderDetailsComponent } from './order-details/order-details.component';

const routes = [
  { path: '', component: OrdersComponent, data: { breadcrumb: 'orders' } },
  { path: ':id', component: OrderDetailsComponent, data: { breadcrumb: { alias: 'order-header-title' } } },
]

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class OrdersRoutingModule { }
