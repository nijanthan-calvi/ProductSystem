import { createReducer, on } from '@ngrx/store';
import { ProductState, initialState } from './product.state';
import * as ProductActions from './product.actions';

export const productFeatureKey = 'products';

export const productReducer = createReducer(
  initialState,
  on(ProductActions.loadProducts, (state: ProductState) => ({
    ...state,
  })),
  on(ProductActions.loadProductsSuccess, (state: ProductState, { products }) => ({
    ...state,
    products,
    filteredProducts: products,
    error: null,
  })),
  on(ProductActions.loadProductsFailure, (state: ProductState, { error }) => ({
    ...state,
    error,
  })),
  on(ProductActions.getProductById, (state: ProductState) => ({
    ...state,
    selectedProduct: null,
  })),
  on(ProductActions.getProductByIdSuccess, (state: ProductState, { product }) => ({
    ...state,
    selectedProduct: product,
    error: null,
  })),
  on(ProductActions.getProductByIdFailure, (state: ProductState, { error }) => ({
    ...state,
    selectedProduct: null,
    error,
  })),
  on(ProductActions.addProductSuccess, (state: ProductState, { product }) => ({
    ...state,
    products: [...state.products, product],
    error: null,
  })),
  on(ProductActions.addProductFailure, (state: ProductState, { error }) => ({
    ...state,
    error,
  })),
  on(ProductActions.updateProductSuccess, (state: ProductState, { product }) => ({
    ...state,
    products: state.products.map((p) => (p.id === product.id ? product : p)),
    error: null,
  })),
  on(ProductActions.updateProductFailure, (state: ProductState, { error }) => ({
    ...state,
    error,
  })),
  on(ProductActions.deleteProduct, (state: ProductState, { id }) => ({
    ...state,
    products: state.products.filter((p) => p.id !== id),
  })),
  on(ProductActions.deleteProductSuccess, (state: ProductState, { id }) => ({
    ...state,
    products: state.products.filter((p) => (p.id !== id )),
    error: null,
  })),
  on(ProductActions.deleteProductFailure, (state: ProductState, { error }) => ({
    ...state,
    error,
  })),
  on(ProductActions.searchProducts, (state: ProductState, { searchTerm }) => ({
    ...state,
    searchTerm,
  })),
  on(ProductActions.sortProducts, (state: ProductState, { sortBy, direction }) => {
    const sortedProducts = [...state.filteredProducts].sort((a, b) => {
      const compareValue =
        sortBy === 'price'
          ? a[sortBy] - b[sortBy]
          : a[sortBy].localeCompare(b[sortBy]);
      return direction === 'asc' ? compareValue : -compareValue;
    });

    return {
      ...state,
      sortBy,
      sortDirection: direction,
      filteredProducts: sortedProducts,
    };
  }),
);
