import { Component, Input } from '@angular/core';
import { BasketDetailsDto } from 'src/app/dtos/local/basket-details.dto';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.css']
})
export class CheckoutReviewComponent {

  @Input() basketDetails?: BasketDetailsDto;

}
