import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { BasketDetailsDto } from 'src/app/dtos/local/basket-details.dto';

@Component({
  selector: 'app-order-summary',
  templateUrl: './order-summary.component.html',
  styleUrls: ['./order-summary.component.css']
})
export class OrderSummaryComponent {

  @Input() subtotal: number = 0;
  @Input() shipping: number = 0;

}
