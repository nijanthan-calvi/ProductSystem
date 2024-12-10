import { createAction, props } from '@ngrx/store';
import { Product } from '../product/product.model';

const PRODUCT_API = '[Product API]';
export const loadProducts = createAction(`${PRODUCT_API}  Load Products`);

export const loadProductsSuccess = createAction(
  `${PRODUCT_API} Load Products Success`, 
  props<{ products: Product[] }>());

export const loadProductsFailure = createAction(
  `${PRODUCT_API} Load Products Failure`, 
  props<{ error: string }>());

export const getProductById = createAction(
  `${PRODUCT_API} Get ProductById`, 
  props<{ id: number }>());

export const getProductByIdSuccess = createAction(
  `${PRODUCT_API} Get ProductById Success`,
  props<{ product: Product }>());

export const getProductByIdFailure = createAction(
  `${PRODUCT_API} Get ProductById Failure`,
  props<{ error: any }>());

export const addProduct = createAction(
  `${PRODUCT_API} Add Product`, 
  props<{ product: Product }>());

export const addProductSuccess = createAction(
  `${PRODUCT_API} Add Product Success`, 
  props<{ product: Product }>());

export const addProductFailure = createAction(
  `${PRODUCT_API} Add Product Failure`, 
  props<{ error: string }>());

export const updateProduct = createAction(
  `${PRODUCT_API} Update Product`, 
  props<{ product: Product }>());

export const updateProductSuccess = createAction(
  `${PRODUCT_API} Update Product Success`, 
  props<{ product: Product }>());

export const updateProductFailure = createAction(
  `${PRODUCT_API} Update Product Failure`, 
  props<{ error: string }>());  

export const deleteProduct = createAction(
  `${PRODUCT_API} Delete Product`, 
  props<{ id: number }>());

export const deleteProductSuccess = createAction(
  `${PRODUCT_API} Delete Product Success`,
  props<{ id: number }>());

export const deleteProductFailure = createAction(
  `${PRODUCT_API} Delete Product Failure`,
  props<{ error: any }>());

export const searchProducts = createAction(
  `${PRODUCT_API} Search Products`,
  props<{ searchTerm: string }>());

export const sortProducts = createAction(
  `${PRODUCT_API} Sort Products`,
  props<{ sortBy: 'name' | 'category' | 'description' | 'price'; direction: 'asc' | 'desc' }>());
