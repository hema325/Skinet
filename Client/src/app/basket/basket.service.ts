import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { BasketDto } from '../dtos/local/basket.dto';
import { BasketProductDto } from '../dtos/local/basket-product.dto';
import { ShopService } from '../shop/shop.service';
import { BasketDetailsDto } from '../dtos/local/basket-details.dto';
import { ProductDto } from '../dtos/responses/product.dto';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  private key = 'basket';
  basket: BasketDto = new BasketDto();
  private basketProductsCountSource = new BehaviorSubject<number>(0);
  basketProductsCountSource$ = this.basketProductsCountSource.asObservable();


  constructor(private shopService: ShopService) {
    this.basket = this.getBasket();
    this.basketProductsCountSource.next(this.getBasketProductsCount());
  }

  private getBasket(): BasketDto {
    const basket = localStorage.getItem(this.key);
    return !basket ? this.basket : JSON.parse(basket);
  }

  private setBasket() {
    const basketJson = JSON.stringify(this.basket);
    localStorage.setItem(this.key, basketJson);
  }

  addProductToBasket(basketProduct: BasketProductDto) {
    const existingProduct = this.basket.basketProducts.find(p => p.productId == basketProduct.productId);
    if (existingProduct)
      existingProduct.quantity += basketProduct.quantity;
    else
      this.basket.basketProducts.push(basketProduct);
    this.setBasket();
    this.basketProductsCountSource.next(this.basketProductsCountSource.value + basketProduct.quantity);
  }

  removeProductFromBasket(productId: number) {
    const product = this.basket.basketProducts.find((p, index) => {
      if (p.productId == productId) {
        this.basket.basketProducts.splice(index, 1);
        return true;
      }
      return false;
    });

    if (product) {
      this.setBasket();
      this.basketProductsCountSource.next(this.basketProductsCountSource.value - product.quantity);
    }
  }

  updateProductBasket(basketProduct: BasketProductDto) {
    const existingProduct = this.basket.basketProducts.find(p => p.productId == basketProduct.productId);

    if (basketProduct.quantity < 1) {
      this.removeProductFromBasket(basketProduct.productId);
      return;
    }

    const oldQuantity: number = Number(existingProduct?.quantity);
    if (existingProduct)
      existingProduct.quantity = basketProduct.quantity;

    else
      this.basket.basketProducts.push(basketProduct);
    this.setBasket();
    this.basketProductsCountSource.next(this.basketProductsCountSource.value - oldQuantity + basketProduct.quantity);
  }


  clearBasket() {
    this.basket = new BasketDto();
    this.setBasket();
    this.basketProductsCountSource.next(0);
  }

  private getBasketProductsCount() {
    return this.basket.basketProducts.reduce((acc, product) => acc += product.quantity, 0);
  }

  getProductCount(productId: number) {
    return this.basket.basketProducts.find(product => product.productId == productId)?.quantity;
  }

  getBasketDetails() {
    return this.shopService
      .getByIds(this.basket.basketProducts.map(p => p.productId))
      .pipe(map(products => {
        let basketDetails = new BasketDetailsDto();
        products.forEach(product => {
          const basketProduct = this.mapProductsToBasketProduc(product);
          basketProduct.quantity = this.basket.basketProducts.find(p => p.productId == product.id)!.quantity;
          basketDetails!.basketProducts.push(basketProduct);
        })

        return basketDetails;
      }));
  }

  private mapProductsToBasketProduc(product: ProductDto) {
    return {
      productId: product.id,
      quantity: 0,
      price: product.price,
      name: product.name,
      pictureUrl: product.pictureUrl,
      categoryName: product.categoryName,
      brandName: product.brandName
    }
  }
}
