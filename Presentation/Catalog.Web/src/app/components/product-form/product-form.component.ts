import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Category } from '../../models/Category.model';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { CategoryService } from '../../services/categoryService';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})

export class ProductFormComponent implements OnInit, OnDestroy {
  form!: FormGroup;
  categories: Category[] = [];
  isEdit = false;
  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private router: Router
  )
  {
    this.initializeForm();
  }

  ngOnInit(): void {

    this.categoryService.categories.pipe(takeUntil(this.destroy$)).subscribe(c => (this.categories = c));

    const id = this.route.snapshot.paramMap.get('id');

    if (id && id !== 'new') {
      this.isEdit = true;
      this.productService.getById(id).pipe(takeUntil(this.destroy$)).subscribe(p => {
        if (p) this.form.patchValue(p);
      });
    }
  }

  ngOnDestroy(): void {

    this.destroy$.next();
    this.destroy$.complete();

  }

  private initializeForm(): void {
    this.form = this.fb.group({
      id: [''],
      name: ['', [Validators.required]],
      description: [''],
      price: [0, [Validators.required, Validators.min(0)]],
      categoryId: [''],
      createdAt: ['']
    });
  }

  onSubmit(): void {

    if (this.form.invalid)
      return;

    const value = this.form.value as Product;

    if (this.isEdit) {
      this.productService.updateProduct(value).pipe(takeUntil(this.destroy$)).subscribe({
        next: () => this.router.navigate(['/'])
      });
    }
    else {
       this.productService.addProduct(value ).pipe(takeUntil(this.destroy$)).subscribe({
        next: () => this.router.navigate(['/'])
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/']);
  }
}
