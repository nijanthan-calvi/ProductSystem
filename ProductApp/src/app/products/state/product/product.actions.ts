import { createAction, props } from '@ngrx/store';
import { Product } from '../product/product.model';

export const loadProducts = createAction('[Product API] Load Products');

export const loadProductsSuccess = createAction(
  '[Product API] Load Products Success', 
  props<{ products: Product[] }>());

export const loadProductsFailure = createAction(
  '[Product API] Load Products Failure', 
  props<{ error: string }>());

export const addProduct = createAction(
  '[Product API] Add Product', 
  props<{ product: Product }>());

export const addProductSuccess = createAction(
  '[Product API] Add Product Success', 
  props<{ product: Product }>());

export const addProductFailure = createAction(
  '[Product API] Add Product Failure', 
  props<{ error: string }>());

export const updateProduct = createAction(
  '[Product API] Update Product', 
  props<{ product: Product }>());

export const updateProductSuccess = createAction(
  '[Product API] Update Product Success', 
  props<{ product: Product }>());

export const updateProductFailure = createAction(
  '[Product API] Update Product Failure', 
  props<{ error: string }>());  

export const deleteProduct = createAction(
  '[Product API] Delete Product', 
  props<{ id: number }>());

export const deleteProductSuccess = createAction(
  '[Product API] Delete Product Success',
  props<{ id: number }>());

export const deleteProductFailure = createAction(
  '[Product API] Delete Product Failure',
  props<{ error: any }>());

export const searchProducts = createAction(
  '[Product List] Search Products',
  props<{ searchTerm: string }>());

export const sortProducts = createAction(
  '[Product List] Sort Products',
  props<{ sortBy: 'name' | 'category' | 'description' | 'price'; direction: 'asc' | 'desc' }>());
