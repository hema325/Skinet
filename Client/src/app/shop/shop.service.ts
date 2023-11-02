import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PaginatedListDto } from '../dtos/responses/paginated-list.dto';
import { ProductDto } from '../dtos/responses/product.dto';
import { BrandDto } from '../dtos/responses/brand.dto';
import { CategoryDto } from '../dtos/responses/category.dto';
import { environment } from 'src/environments/environment.development';
import { ProductFilterDto } from '../dtos/requests/product/product-filter.dto';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = environment.baseUrl;
  filterParams = new ProductFilterDto();

  constructor(private httpClient: HttpClient) { }

  getProducts() {

    let params = new HttpParams();
    params = this.filterParams.brandId ? params.append('brandId', this.filterParams.brandId) : params;
    params = this.filterParams.categoryId ? params.append('categoryId', this.filterParams.categoryId) : params;
    params = this.filterParams.search ? params.append('search', this.filterParams.search) : params;
    params = this.filterParams.sort ? params.append('sort', this.filterParams.sort) : params;
    params = params.append('pageNumber', this.filterParams.pageNumber);
    params = params.append('pageSize', this.filterParams.pageSize);

    return this.httpClient.get<PaginatedListDto<ProductDto>>(`${this.baseUrl}/products`, { params });
  }

  getByIds(ids: number[]) {
    let params = new HttpParams();
    ids.forEach(id => {
      params = params.append('ids', id);
    });
    return this.httpClient.get<ProductDto[]>(this.baseUrl + '/products/findByIds', { params });
  }

  getProduct(id: number) {
    return this.httpClient.get<ProductDto>(`${this.baseUrl}/products/${id}`);
  }

  getBrands() {
    return this.httpClient.get<BrandDto[]>(`${this.baseUrl}/brands`);
  }

  getCategories() {
    return this.httpClient.get<CategoryDto[]>(`${this.baseUrl}/categories`);
  }

  resetFilterParams() {
    this.filterParams = new ProductFilterDto();
    return this.filterParams;
  }

}
