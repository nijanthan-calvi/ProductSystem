import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { Store } from '@ngrx/store';
import { Product } from '../product.model';
import * as ProductActions from '../state/product.actions';
import { selectFilteredProducts, selectProducts } from '../state/product.selectors';
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

  constructor(
    private fb: FormBuilder,
    private store: Store<{ products: Product[] }>,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      category: ['', Validators.required],
      price: [0, Validators.required],
    });
  }

  ngOnInit(): void {
    this.store.dispatch(ProductActions.loadProducts());
    this.productId = +this.route.snapshot.paramMap.get('id')!;
    this.isEditMode = !!this.productId;

    if (this.isEditMode) {
      // Load product details for editing
      this.store.select(selectProducts).subscribe((products) => {
        const product = products.find((p) => p.id === this.productId);
        if (product) {
          this.productForm.patchValue(product);
        }
      });
    }
  }

  onSubmit(): void {
    const product: Product = this.productForm.value;

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
