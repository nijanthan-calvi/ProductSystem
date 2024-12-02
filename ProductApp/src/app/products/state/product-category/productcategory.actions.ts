import { createAction, props } from '@ngrx/store';
import { ProductCategory } from '../product-category/productcategory.model';

export const loadCategories = createAction('[Product API] Load Categories');

export const loadCategoriesSuccess = createAction(
  '[Product API] Load Categories Success', 
  props<{ productcategories: ProductCategory[] }>());

export const loadCategoriesFailure = createAction(
  '[Product API] Load Categories Failure', 
  props<{ error: string }>());
