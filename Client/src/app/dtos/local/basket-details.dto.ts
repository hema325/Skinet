
export interface BasketDetailsDto {
    basketProducts: basketProductDetailsDto[];
}

export class BasketDetailsDto implements BasketDetailsDto {
    basketProducts: basketProductDetailsDto[] = [];
}

export interface basketProductDetailsDto {
    productId: number,
    quantity: number,
    price: number,
    name: string
    pictureUrl: string
    categoryName: string
    brandName: string
}
