import { Routes } from '@angular/router';
import { ProductListComponent } from './products/product-list/product-list.component';
import { ProductFormComponent } from './products/product-form/product-form.component';

export const routes: Routes = [
    { path: '', redirectTo: 'products', pathMatch: 'full' }, // Redirect to products
    { path: 'products', component: ProductListComponent, runGuardsAndResolvers: 'pathParamsChange' },
    { path: 'products/add', component: ProductFormComponent },
    { path: 'products/edit/:id', component: ProductFormComponent },
];
