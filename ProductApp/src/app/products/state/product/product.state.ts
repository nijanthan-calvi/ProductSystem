import { Product } from "../product/product.model";

export interface ProductState {
    products: Product[];
    filteredProducts: Product[];
    selectedProduct: Product | null,
    searchTerm: string;
    sortBy: string;
    sortDirection: string;
    error: string | null;
  }
  
  export const initialState: ProductState = {
    products: [],
    filteredProducts: [],
    selectedProduct: null,
    searchTerm: '',  
    sortBy: '',  
    sortDirection: '',
    error: null,
  };