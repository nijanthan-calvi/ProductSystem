import { createAction, props } from '@ngrx/store';
import { ProductCategory } from '../product-category/productcategory.model';

const PRODUCT_API = '[Product API]';
export const loadCategories = createAction(`${PRODUCT_API}  Load Categories`);

export const loadCategoriesSuccess = createAction(
  `${PRODUCT_API} Load Categories Success`, 
  props<{ productcategories: ProductCategory[] }>());

export const loadCategoriesFailure = createAction(
  `${PRODUCT_API} Load Categories Failure`, 
  props<{ error: string }>());
