import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-checkout-success',
  templateUrl: './checkout-success.component.html',
  styleUrls: ['./checkout-success.component.css']
})
export class CheckoutSuccessComponent {

  orderId: number;

  constructor(private router: Router) {
    this.orderId = this.router.getCurrentNavigation()?.extras?.state?.['orderId'] || 0;
  }
}
