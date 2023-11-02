import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CheckoutService } from '../checkout.service';
import { DeliveryMethodDto } from 'src/app/dtos/responses/delivery-method.dto';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.css']
})
export class CheckoutDeliveryComponent implements OnInit {
  @Input() deliveryForm!: any;
  deliveryMethods: DeliveryMethodDto[] = [];
  @Output() deliveryChanged = new EventEmitter<DeliveryMethodDto>();

  constructor(private checkoutService: CheckoutService) { }

  ngOnInit(): void {
    this.checkoutService.getDeliveryMethods().subscribe(response => this.deliveryMethods = response);
  }

  updateShippingPrice(method: DeliveryMethodDto) {
    this.deliveryChanged.emit(method);
  }
}
