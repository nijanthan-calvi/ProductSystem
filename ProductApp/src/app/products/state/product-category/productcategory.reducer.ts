import { createReducer, on } from '@ngrx/store';
import * as ProductCategoryActions from './productcategory.actions';
import { initialState } from './productcategory.state';

export const productcategoryFeatureKey = 'productcategories';

export const productcategoryReducer = createReducer(
  initialState,
  on(ProductCategoryActions.loadCategories, (state) => ({
    ...state,
    loading: true,
    error: null,
  })),
  on(ProductCategoryActions.loadCategoriesSuccess, (state, { productcategories }) => ({
    ...state,
    productcategories,
    loading: false,
    error: null,
  })),
  on(ProductCategoryActions.loadCategoriesFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error,
  })),
);
