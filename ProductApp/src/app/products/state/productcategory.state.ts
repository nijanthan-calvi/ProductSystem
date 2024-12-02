import { ProductCategory } from "../productcategory.model";

export interface ProductCategoryState {
    productcategories: ProductCategory[];
    loading: boolean;
    error: string | null;
  }
  
  export const initialState: ProductCategoryState = {
    productcategories: [],
    loading: false,
    error: null,
  };