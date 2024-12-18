import { createReducer, on } from '@ngrx/store';
import * as ProductCategoryActions from './productcategory.actions';
import { initialState, ProductCategoryState } from './productcategory.state';

export const productcategoryFeatureKey = 'productcategories';

export const productcategoryReducer = createReducer(
  initialState,
  on(ProductCategoryActions.loadCategories, (state: ProductCategoryState) => ({
    ...state,
    loading: true,
    error: null,
  })),
  on(ProductCategoryActions.loadCategoriesSuccess, (state: ProductCategoryState, { productcategories }) => ({
    ...state,
    productcategories,
    loading: false,
    error: null,
  })),
  on(ProductCategoryActions.loadCategoriesFailure, (state: ProductCategoryState, { error }) => ({
    ...state,
    loading: false,
    error,
  })),
);
