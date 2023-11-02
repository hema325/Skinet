import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BasketDetailsDto } from 'src/app/dtos/local/basket-details.dto';
import { BasketProductDto } from 'src/app/dtos/local/basket-product.dto';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.css']
})
export class BasketSummaryComponent {

  @Input() showControls = false;
  @Input() basketDetails?: BasketDetailsDto;
  @Output() productCountChanged = new EventEmitter<BasketProductDto>();

  increaseProductCount(product: BasketProductDto) {
    product.quantity += 1;
    this.productCountChanged.emit(product);
  }

  decreaseProductCount(product: BasketProductDto) {
    if (product.quantity > 0)
      product.quantity -= 1;
    this.productCountChanged.emit(product);
  }

  removeBasketProduct(product: BasketProductDto) {
    product.quantity = 0;
    this.productCountChanged.emit(product);
  }

}
