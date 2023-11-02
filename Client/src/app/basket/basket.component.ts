import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket.service';
import { ShopService } from '../shop/shop.service';
import { BasketDetailsDto } from '../dtos/local/basket-details.dto';
import { ProductDto } from '../dtos/responses/product.dto';
import { BasketProductDto } from '../dtos/local/basket-product.dto';
import { take } from 'rxjs';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit {
  basketDetails?: BasketDetailsDto;
  subtotal: number = 0;

  constructor(private basketService: BasketService, private productsService: ShopService) { }

  ngOnInit(): void {
    this.loadProductDetails();
  }

  loadProductDetails() {
    this.basketService.getBasketDetails().pipe(take(1)).subscribe(detials => {
      this.basketDetails = detials;
      this.subtotal = detials.basketProducts.reduce((acc, pro) => acc + pro.price * pro.quantity, 0);
      console.log(detials.basketProducts);
    });
  }

  updateProductCount(product: BasketProductDto) {
    this.basketService.updateProductBasket(product);
    if (product.quantity < 1)
      this.loadProductDetails();
    this.subtotal = this.basketDetails?.basketProducts.reduce((acc, pro) => acc + pro.price * pro.quantity, 0) || 0;
  }

}
