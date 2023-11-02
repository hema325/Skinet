import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ProductDto } from '../dtos/responses/product.dto';
import { CategoryDto } from '../dtos/responses/category.dto';
import { BrandDto } from '../dtos/responses/brand.dto';
import { ProductFilterDto } from '../dtos/requests/product/product-filter.dto';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {

  products: ProductDto[] = [];
  brands: BrandDto[] = [];
  categories: CategoryDto[] = [];
  filterParams: ProductFilterDto;

  totalPages: number = 0;
  pageSize: number = 0;
  pageNumber: number = 0;

  @ViewChild('searchInput') searchInput!: ElementRef;

  sortOptions = [
    { name: 'Default Sort', value: '' },
    { name: 'Price: Low to high', value: 'priceAsc' },
    { name: 'Price: High to low', value: 'priceDesc' }
  ]

  constructor(private productsService: ShopService) {
    this.filterParams = productsService.filterParams;
  }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getCategories();
  }

  getProducts() {
    this.productsService.getProducts().subscribe({
      next: response => {
        this.products = response.data;
        this.totalPages = response.totalPages;
        this.pageNumber = response.pageNumber;
        this.pageSize = response.pageSize;
      },
      error: error => console.log(error)
    });
  }

  getBrands() {
    this.productsService.getBrands().subscribe({
      next: response => this.brands = [{ id: 0, name: 'All' }, ...response],
      error: error => console.log(error)
    });
  }

  getCategories() {
    this.productsService.getCategories().subscribe({
      next: response => this.categories = [{ id: 0, name: 'All' }, ...response],
      error: error => console.log(error)
    });
  }

  onCategorySelected(categoryId: number) {
    this.filterParams.categoryId = categoryId;
    this.filterParams.pageNumber = 1;
    this.getProducts();
  }

  onBrandSelected(brandId: number) {
    this.filterParams.brandId = brandId;
    this.filterParams.pageNumber = 1;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.filterParams.sort = sort;
    this.filterParams.pageNumber = 1;
    this.getProducts();
  }

  onSearch(search: string) {
    this.filterParams.search = search;
    this.filterParams.pageNumber = 1;
    this.getProducts();
  }

  onReset() {
    this.searchInput.nativeElement.value = '';
    this.filterParams = this.productsService.resetFilterParams();
    this.getProducts();
  }

  onPageChanged(pageNumber: number) {
    this.filterParams.pageNumber = pageNumber;
    this.getProducts();
  }
}

