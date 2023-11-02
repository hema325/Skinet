import { NgModule } from '@angular/core';
import { CheckoutComponent } from './checkout.component';
import { RouterModule } from '@angular/router';
import { CheckoutSuccessComponent } from './checkout-success/checkout-success.component';


const routes = [
  { path: '', component: CheckoutComponent, data: { breadcrumb: { label: 'checkout' } } },
  { path: 'success', component: CheckoutSuccessComponent, data: { breadcrumb: { label: 'success' } } },
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class CheckoutRoutingModule { }
