import { Product } from "../product/product.model";

export interface ProductState {
    products: Product[];
    filteredProducts: Product[];
    searchTerm: string;
    sortBy: string;
    sortDirection: string;
    error: string | null;
  }
  
  export const initialState: ProductState = {
    products: [],
    filteredProducts: [],
    searchTerm: '',  
    sortBy: '',  
    sortDirection: '',
    error: null,
  };