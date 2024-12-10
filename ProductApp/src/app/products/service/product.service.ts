import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Product } from '../state/product/product.model';
import { Observable } from 'rxjs';
import { ProductCategory } from '../state/product-category/productcategory.model';

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
    const headers = this.getAuthHeader(); 
    if (searchTerm) params.search = searchTerm;
    if (sortBy) params.sort = sortBy;
    if (direction) params.direction = direction;

    return this.http.get<Product[]>(this.apiUrl + 'Product', { headers, params });
  }

  getProductById(id: number): Observable<Product> {
    const headers = this.getAuthHeader(); 
    return this.http.get<Product>(`${this.apiUrl + 'Product'}/${id}`, { headers });
  }

  getProductCategories(): Observable<ProductCategory[]> {
    const params: any = {};
    const headers = this.getAuthHeader(); 

    return this.http.get<ProductCategory[]>(this.apiUrl + 'ProductCategory', { headers, params });
  }

  addProduct(product: Product): Observable<Product> {
    const headers = this.getAuthHeader(); 
    return this.http.post<Product>(this.apiUrl + 'Product', product, { headers });
  }

  updateProduct(product: Product): Observable<void> {
    const headers = this.getAuthHeader(); 
    return this.http.put<void>(`${this.apiUrl + 'Product'}/${product.id}`, product, { headers });
  }

  deleteProduct(id: number): Observable<void> {
    const headers = this.getAuthHeader(); 
    return this.http.delete<void>(`${this.apiUrl + 'Product'}/${id}`, { headers });
  }

  private getAuthHeader(): HttpHeaders {
    const credentials = btoa(`${'admin'}:${'password'}`);
    return new HttpHeaders({
      Authorization: `Basic ${credentials}`,
    });
  }
}
