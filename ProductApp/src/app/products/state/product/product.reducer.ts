import { createReducer, on } from '@ngrx/store';
import { ProductState, initialState } from './product.state';
import * as ProductActions from './product.actions';

export const productFeatureKey = 'products';

export const productReducer = createReducer(
  initialState,
  on(ProductActions.loadProducts, (state) => ({
    ...state,
  })),
  on(ProductActions.loadProductsSuccess, (state, { products }) => ({
    ...state,
    products,
    filteredProducts: products,
    error: null,
  })),
  on(ProductActions.loadProductsFailure, (state, { error }) => ({
    ...state,
    error,
  })),
  on(ProductActions.getProductById, (state) => ({
    ...state,
    selectedProduct: null,
  })),
  on(ProductActions.getProductByIdSuccess, (state: ProductState, { product }) => ({
    ...state,
    selectedProduct: product,
    error: null,
  })),
  on(ProductActions.getProductByIdFailure, (state, { error }) => ({
    ...state,
    selectedProduct: null,
    error,
  })),
  on(ProductActions.addProductSuccess, (state: ProductState, { product }) => ({
    ...state,
    products: [...state.products, product],
    error: null,
  })),
  on(ProductActions.addProductFailure, (state, { error }) => ({
    ...state,
    error,
  })),
  on(ProductActions.updateProductSuccess, (state: ProductState, { product }) => ({
    ...state,
    products: state.products.map((p) => (p.id === product.id ? product : p)),
    error: null,
  })),
  on(ProductActions.updateProductFailure, (state, { error }) => ({
    ...state,
    error,
  })),
  on(ProductActions.deleteProduct, (state, { id }) => ({
    ...state,
    products: state.products.filter((p) => p.id !== id),
  })),
  on(ProductActions.deleteProductSuccess, (state, { id }) => ({
    ...state,
    products: state.products.filter((p) => (p.id !== id )),
    error: null,
  })),
  on(ProductActions.deleteProductFailure, (state, { error }) => ({
    ...state,
    error,
  })),
  on(ProductActions.searchProducts, (state, { searchTerm }) => ({
    ...state,
    searchTerm,
    filteredProducts: state.products.filter((product) =>
      product.name.toLowerCase().includes(searchTerm.toLowerCase()))
  })),
  on(ProductActions.sortProducts, (state, { sortBy, direction }) => {
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
