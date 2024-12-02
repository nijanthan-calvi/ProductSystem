import { createFeatureSelector, createSelector } from '@ngrx/store';
import { ProductState } from './product.state';
import * as fromProduct from '../product/product.reducer';

export const selectProductState = createFeatureSelector<ProductState>(
  fromProduct.productFeatureKey
);

export const selectProducts = createSelector(
  selectProductState,
  (state: ProductState) => state.products
);

export const selectSearchTerm = createSelector(
  selectProductState,
  (state: ProductState) => state.searchTerm
);

export const selectFilteredProducts = createSelector(
  selectProductState,
  (state: ProductState) => state.filteredProducts
);



