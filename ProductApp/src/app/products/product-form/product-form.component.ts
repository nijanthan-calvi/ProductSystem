import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { Store } from '@ngrx/store';
import { Product } from '../product.model';
import { ProductCategory } from '../productcategory.model';
import * as ProductActions from '../state/product.actions';
import * as ProductCategoryActions from '../state/productcategory.actions';
import { selectFilteredProducts, selectProducts } from '../state/product.selectors';
import { selectProductCategories, selectProductCategoryLoading, selectProductCategoryError } from '../state/productcategory.selectors';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.scss'
})
export class ProductFormComponent  implements OnInit {
  productForm: FormGroup;
  isEditMode = false;
  productId?: number;
  products$: Observable<Product[]>;
  categories$: Observable<ProductCategory[]>;
  loading$: Observable<boolean>;
  error$: Observable<any>;
  selectedProduct: Product | null = null;
  selectedCategoryName: string | null = null;

  constructor(
    private fb: FormBuilder,
    private store: Store,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.products$ = this.store.select(selectProducts);
    this.categories$ = this.store.select(selectProductCategories);
    this.loading$ = this.store.select(selectProductCategoryLoading);
    this.error$ = this.store.select(selectProductCategoryError);
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      category: [null, Validators.required],
      price: [0, Validators.required],
    });
  }

  ngOnInit(): void {
    this.store.dispatch(ProductCategoryActions.loadCategories());
    this.store.dispatch(ProductActions.loadProducts());
    this.productId = +this.route.snapshot.paramMap.get('id')!;
    this.isEditMode = !!this.productId;

    if (this.isEditMode) {
      // Load product details for editing
      this.store.select(selectProducts).subscribe((products) => {
        const product = products.find((p) => p.id === this.productId);
        if (product) {
          this.productForm.patchValue(product);
          this.selectedProduct = product;
          this.selectedCategoryName = product.category;
        }
      });
    }
  }

  onCategorySelect(category: ProductCategory): void {
    this.selectedCategoryName = category.name;
    this.productForm.controls['category'].setValue(category.id);
  }

  onSubmit(): void {
    const product: Product = this.productForm.value;
    product.category = this.selectedCategoryName!;

    if (this.isEditMode) {
      product.id = this.productId!;
      this.store.dispatch(ProductActions.updateProduct({ product }));
    } else {
      this.store.dispatch(ProductActions.addProduct({ product }));
    }

    this.router.navigate(['/']);
  }

  cancel(): void {
    this.router.navigate(['/']);
  }

}
