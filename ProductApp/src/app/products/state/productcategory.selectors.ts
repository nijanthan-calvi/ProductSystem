import { createFeatureSelector, createSelector } from '@ngrx/store';
import { ProductCategoryState } from './productcategory.state';
import * as fromProductCategory from './productcategory.reducer';

export const selectProductCategoryState = createFeatureSelector<ProductCategoryState>(
  fromProductCategory.productcategoryFeatureKey
);

export const selectProductCategories = createSelector(
  selectProductCategoryState,
  (state: ProductCategoryState) => state.productcategories
);

export const selectProductCategoryLoading = createSelector(
  selectProductCategoryState,
  (state: ProductCategoryState) => state.loading
);

export const selectProductCategoryError = createSelector(
  selectProductCategoryState,
  (state: ProductCategoryState) => state.error
);



