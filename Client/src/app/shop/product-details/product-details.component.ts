import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { ProductDto } from 'src/app/dtos/responses/product.dto';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {

  product?: ProductDto;
  quantity = 0;
  quantityInBasket = 0;
  constructor(private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService,
    private basketService: BasketService,
    private toastrService: ToastrService) {
    this.bcService.set('@productDetails', ' ');
  }

  ngOnInit(): void {
    this.getProduct();
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this.basketService.getProductCount(+id)
      const productCount = this.basketService.getProductCount(+id);
      this.quantity = this.quantityInBasket = productCount ? productCount : 0;
    }
  }

  getProduct() {
    const id = this.activatedRoute.snapshot.params['id'];
    this.shopService.getProduct(+id).subscribe({
      next: response => {
        this.product = response;
        this.bcService.set('@productDetails', response.name);
      },
      error: error => console.log(error)
    });
  }

  addToBasket() {
    if (!this.product)
      return;

    this.quantityInBasket = this.quantity;
    this.basketService.addProductToBasket({
      productId: this.product.id,
      quantity: this.quantity
    });
    this.toastrService.success('Product is added to the basket successfully');
  }

  updateProductBasketCount() {
    if (!this.product) return;
    this.basketService.updateProductBasket({
      productId: this.product.id,
      quantity: this.quantity
    });
    this.quantityInBasket = this.quantity;
    this.toastrService.success('Basket is updated successfully');
  }

  increaseQuantity() {
    ++this.quantity;
  }

  decreaseQuantity() {
    if (this.quantity > 0)
      --this.quantity;
  }



}
