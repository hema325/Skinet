import { BasketProductDto } from "./basket-product.dto"

export interface BasketDto {
    basketProducts: BasketProductDto[],
    shippingMethodId: number
}

export class BasketDto implements BasketDto {
    basketProducts: BasketProductDto[] = [];
}
