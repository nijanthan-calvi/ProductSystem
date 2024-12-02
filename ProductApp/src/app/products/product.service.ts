import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from './state/product/product.model';
import { Observable } from 'rxjs';
import { ProductCategory } from './state/product-category/productcategory.model';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private apiUrl = 'https://localhost:7062/api/';

  constructor(private http: HttpClient) {}

  getProducts(
    searchTerm: string = '',
    sortBy: string = '',
    direction: string = ''
  ): Observable<Product[]> {
    const params: any = {};
    if (searchTerm) params.search = searchTerm;
    if (sortBy) params.sort = sortBy;
    if (direction) params.direction = direction;

    return this.http.get<Product[]>(this.apiUrl + 'Product', { params });
  }

  getProductCategories(): Observable<ProductCategory[]> {
    const params: any = {};

    return this.http.get<ProductCategory[]>(this.apiUrl + 'ProductCategory', { params });
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl + 'Product', product);
  }

  updateProduct(product: Product): Observable<void> {
    return this.http.put<void>(`${this.apiUrl + 'Product'}/${product.id}`, product);
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl + 'Product'}/${id}`);
  }
}
