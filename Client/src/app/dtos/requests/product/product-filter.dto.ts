

export interface ProductFilterDto {
    brandId: number,
    categoryId: number,
    search: string,
    sort: string,
    pageNumber: number,
    pageSize: number,
}

export class ProductFilterDto implements ProductFilterDto {
    brandId = 0;
    categoryId = 0;
    search = '';
    sort = '';
    pageNumber = 1;
    pageSize = 6;
}
