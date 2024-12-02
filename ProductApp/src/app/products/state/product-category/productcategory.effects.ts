import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap } from 'rxjs/operators';
import * as ProductCategoryActions  from './productcategory.actions';
import { of } from 'rxjs';
import { ProductService } from '../../product.service';

@Injectable()
export class ProductCategoryEffects {
  constructor(private actions$: Actions, private productService: ProductService) {}

  loadCategories$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProductCategoryActions.loadCategories),
      mergeMap(() =>
        this.productService.getProductCategories().pipe(
          map((productcategories) => ProductCategoryActions.loadCategoriesSuccess({ productcategories })),
          catchError((error) => of(ProductCategoryActions.loadCategoriesFailure({ error })))
        )
      )
    )
  );
}
