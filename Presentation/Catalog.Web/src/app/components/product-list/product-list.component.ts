import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { BehaviorSubject, debounceTime, distinctUntilChanged, switchMap, of, Subject } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop'
import { Product } from '../../models/product.model';
import { SearchBarComponent } from '../search-bar/search-bar.component';
import { CategoryFilterComponent } from '../category-filter/category-filter.component';
import { ProductService } from '../../services/product.service';
import { CategoryService } from '../../services/categoryService';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
    selector: 'app-product-list',
    standalone: true,
    imports: [MatProgressSpinnerModule,CommonModule, SearchBarComponent, CategoryFilterComponent],
    templateUrl: './product-list.component.html',
    styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnDestroy, OnInit {

    products: Product[] = [];
    currentPage: number = 1;
    pageSize: number = 10; // Items per page
    pagedProducts: Product[] = []; // Only the items for the current view
    categoriesMap: Record<string, string> = {};
    categoryId: string | undefined = undefined; 

    loading = false;
    loadProductsError = false;

    private readonly refresh$ = new BehaviorSubject<void>(undefined); 
    private readonly searchTerm$ = new Subject<string>();
    private readonly categoryId$ = new Subject<string | undefined>();

    constructor(private productService: ProductService, private categoryService: CategoryService, private router: Router) {
        // initialise refresh subscription  
        this.refresh$
            .pipe(takeUntilDestroyed())
            .subscribe(() => this.loadProducts(this.categoryId));

        // initialise categories map    
        this.categoryService.categories
            .pipe(takeUntilDestroyed()
            ).subscribe(categories => categories.forEach(element => this.categoriesMap[element.id] = element.name));

        // Combine search, category filter, and refresh observables     
        this.categoryId$
            .pipe(takeUntilDestroyed())
            .subscribe(() => this.loadProducts(this.categoryId));

    }

    ngOnInit(): void {
        this.configureSearch();
    }

    configureSearch() {

        this.searchTerm$.pipe(
            // 1. Wait for 400ms of "non-activeness"
            debounceTime(800),

            // 2. Only search if the term is different from the last one
            distinctUntilChanged(),

            // 3. Switch to the search call (cancels previous if user types again)
            switchMap(term => {
                this.loading = true;
                return this.productService.search(term);
            })
        ).subscribe({
            next: (products) => {
                this.currentPage = 1;
                this.products = products;
                this.loading = false;
                this.updatePagedProducts();
            },
            error: (error) => {
                this.loading = false;
                console.error('Error fetching products:', error);
            }
        });
    }

    loadProducts(categoryId: string | undefined = undefined) {

        if (categoryId) {
            this.loadFilteredProducts(categoryId);
        }
        else {
            this.loading = true;
            this.productService.getProducts().subscribe(
                {
                    next: (products) => {
                        this.currentPage = 1;
                        this.products = products;
                        this.loading = false;
                        this.updatePagedProducts();
                    },
                    error: (error) => {
                        console.error('Error fetching products:', error);
                        this.loading = false;
                    }
                });
        }
    }

   loadFilteredProducts( categoryId: string ) {

        this.loading = true;
        this.productService.getFilterdProducts(categoryId).subscribe(
            {
                next: (products) => {
                    this.currentPage = 1;  
                    this.products = products;
                    this.loading = false;
                    this.updatePagedProducts();
                },
                error: (error) => {
                    console.error('Error fetching products:', error);
                    this.loading = false;
                }
            });
    }

    refreshData() {
        this.refresh$.next();
    }

    onSearch(term: string) {       
        console.log('Search term:', term);      
        this.searchTerm$.next(term || '');
    }

    onCategory(id?: string) {
        this.categoryId = id;
        this.categoryId$.next(id);
    }

    onAdd() {
        this.router.navigate(['/product/new']);
    }

    onEdit(id: string) {
        this.router.navigate(['/product', id]);
    }

    onDelete(id: string) {

        if (!confirm('Delete this product?')) return;

        this.productService.deleteProduct(id).subscribe({
            next: () => { this.refreshData() },
            error: e => alert('Delete failed: ' + (e?.message || e))
        });

    }

    // Call this whenever data is loaded or page changes
    updatePagedProducts() {
        const startIndex = (this.currentPage - 1) * this.pageSize;
        const endIndex = startIndex + this.pageSize;
        this.pagedProducts = this.products.slice(startIndex, endIndex);
    }

    // Navigation methods
    goToPage(page: number) {
        this.currentPage = page;
        this.updatePagedProducts();
    }

    get totalPages(): number {
        return Math.ceil(this.products.length / this.pageSize);
    }

    ngOnDestroy(): void {
        // this.sub.unsubscribe();
    }
}
