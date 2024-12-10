import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ProductService } from '../../service/product.service';
import * as ProductActions from './product.actions';
import { catchError, map, mergeMap, tap } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable()
export class ProductEffects {
  constructor(private actions$: Actions, private productService: ProductService) {}

  loadProducts$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProductActions.loadProducts),
      mergeMap(() =>
        this.productService.getProducts().pipe(
          map((products) => ProductActions.loadProductsSuccess({ products })),
          catchError((error) => of(ProductActions.loadProductsFailure({ error })))
        )
      )
    )
  );

  getProductById$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProductActions.getProductById),
      mergeMap(({ id }) =>
        this.productService.getProductById(id).pipe(
          map((product) =>
            ProductActions.getProductByIdSuccess({ product })
          ),
          catchError((error) =>
            of(ProductActions.getProductByIdFailure({ error }))
          )
        )
      )
    )
  );

  addProduct$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProductActions.addProduct),
      mergeMap(({ product }) =>
        this.productService.addProduct(product).pipe(
          map((newProduct) => ProductActions.addProductSuccess({ product: newProduct })),
          catchError((error) => of(ProductActions.addProductFailure({ error })))
        )
      )
    )
  );

  updateProduct$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProductActions.updateProduct),
      mergeMap(({ product }) =>
        this.productService.updateProduct(product).pipe(
          map(() => ProductActions.updateProductSuccess({ product })),
          catchError((error) => of(ProductActions.updateProductFailure({ error })))
        )
      )
    )
  );

  deleteProduct$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProductActions.deleteProduct), // Listen for the deleteProduct action
      mergeMap(({ id }) =>
        this.productService.deleteProduct(id).pipe(
          map(() =>
            ProductActions.deleteProductSuccess({ id }) // Dispatch success action
          ),
          catchError((error) =>
            of(ProductActions.deleteProductFailure({ error })) // Dispatch failure action
          )
        )
      )
    )
  );

  searchAndSortProducts$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProductActions.sortProducts),
      mergeMap(({ sortBy, direction }: any) =>
        this.productService.getProducts('', sortBy, direction).pipe(
          map((products) =>
            ProductActions.loadProductsSuccess({ products })
          ),
          catchError((error) =>
            of(ProductActions.loadProductsFailure({ error }))
          )
        )
      )
    )
  );

  // Centralized failure handling
  handleFailures$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(
          ProductActions.loadProductsFailure,
          ProductActions.addProductFailure,
          ProductActions.updateProductFailure,
          ProductActions.deleteProductFailure
        ),
        tap((action) => {
          console.error('API Failure:', action.error);
          alert(`An error occurred: ${action.error}`); 
        })
      ),
    { dispatch: false } 
  );
}
