import { Component, computed, OnInit, signal } from '@angular/core';
import { Store } from '@ngrx/store';
import { Product } from '../state/product/product.model';
import * as ProductActions from '../state/product/product.actions';
import { selectProducts } from '../state/product/product.selectors';
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
  products = signal<Product[]>([]);
  searchTerm = signal<string>('');
  sortDirection: string | undefined;
  selectedProductId: number | null = null;

  filteredProducts = computed(() => 
    this.products().filter((product) => 
      product.name.toLowerCase().includes(this.searchTerm().toLowerCase())));

  constructor(private store: Store) {}

  ngOnInit(): void {
    this.store.dispatch(ProductActions.loadProducts());

    this.store.select(selectProducts).subscribe((products) => {
      this.products.set(products);
    })
  }

  setSelectedProductId(id: number): void {
    this.selectedProductId = id;
  }

  onSearch(searchTerm: string): void {
    this.searchTerm.set(searchTerm);
  }

  onSort(sortBy: 'name' | 'category' | 'description' | 'price'): void {
    const direction = this.sortDirection === 'asc' ? 'desc' : 'asc';
    this.sortDirection = direction;
    this.store.dispatch(ProductActions.sortProducts({ sortBy, direction }));
  }

  editProduct(id: number): void {
    // Navigate to the edit page
    window.location.href = `/products/form/edit/${id}`;
  }

  deleteProduct(id: number | null): void {
    if (id !== null) 
    {
      this.store.dispatch(ProductActions.deleteProduct({ id }));
    }
  }
}
