import { Component, Input } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';
import { ProductDto } from 'src/app/dtos/responses/product.dto';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.css']
})
export class ProductItemComponent {

  constructor(private basketService: BasketService,
    private toastrService: ToastrService) { }
  @Input() productItem!: ProductDto;

  addToBasket() {
    this.basketService.addProductToBasket({
      productId: this.productItem.id,
      quantity: 1
    });
    this.toastrService.success('Product is added to the basket successfully');
  }
}
