import { Routes } from '@angular/router';
import { ProductListComponent } from './products/product-list/product-list.component';

export const routes: Routes = [
    { path: '', redirectTo: 'products', pathMatch: 'full' },
    { path: 'products', component: ProductListComponent, runGuardsAndResolvers: 'pathParamsChange' },
    {
      path: 'products/form',
      loadChildren: () =>
        import('./products/product-form/product-form-routing.module').then((m) => m.ProductFormRoutingModule),
    },
  ];
  