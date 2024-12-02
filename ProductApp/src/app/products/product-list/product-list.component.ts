import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Product } from '../product.model';
import * as ProductActions from '../state/product.actions';
import { selectFilteredProducts, selectProducts, selectSearchTerm } from '../state/product.selectors';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
})
export class ProductListComponent implements OnInit {
  products$: Observable<Product[]>;
  searchTerm$: Observable<string>;
  sortDirection: string | undefined;
  selectedProductId: number | null = null;

  constructor(private store: Store) {
    this.products$ = this.store.select(selectProducts);
    this.searchTerm$ = this.store.select(selectSearchTerm);
  }

  ngOnInit(): void {
    this.store.dispatch(ProductActions.loadProducts());
  }

  setSelectedProductId(id: number): void {
    this.selectedProductId = id;
  }

  onSearch(searchTerm: string): void {
    this.store.dispatch(ProductActions.searchProducts({ searchTerm }));
  }

  onSort(sortBy: 'name' | 'category' | 'description' | 'price'): void {
    const direction = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.sortDirection = direction;
    this.store.dispatch(ProductActions.sortProducts({ sortBy, direction }));
  }

  editProduct(id: number): void {
    // Navigate to the edit page
    window.location.href = `/products/edit/${id}`;
  }

  deleteProduct(id: number | null): void {
    if (id !== null) 
    {
      this.store.dispatch(ProductActions.deleteProduct({ id }));
    }
  }
}
