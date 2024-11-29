import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { productReducer } from './products/state/product.reducer';

import { routes } from './app.routes';
import { provideStore, Store, StoreModule } from '@ngrx/store';
import { EffectsModule, provideEffects } from '@ngrx/effects';
import { HttpClientModule } from '@angular/common/http';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { ProductEffects } from './products/state/product.effects';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes), 
    provideStore(), 
    provideEffects(),
    importProvidersFrom(
      HttpClientModule,
      EffectsModule.forRoot({}),
      StoreModule.forRoot({}),
      StoreModule.forFeature('products', productReducer),
      EffectsModule.forFeature(ProductEffects),
      StoreDevtoolsModule.instrument({
        maxAge: 30,
        logOnly: false,
      }),
    )],
};
