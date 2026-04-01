import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { Category } from "../models/Category.model";
import { environment } from "../../environments/environment";

@Injectable({ providedIn: 'root' })

export class CategoryService {
  private http = inject(HttpClient);
  
  private categoriesUrl = `${environment.apiUrl}/categories`;

  private categoriesSubject = new BehaviorSubject<Category[]>([]);
  categories$ = this.categoriesSubject.asObservable();

  constructor() {
    this.loadCategories();
  }

  private loadCategories(): void {

    this.http.get<Category[]>(this.categoriesUrl).subscribe({
      next: (data) => this.categoriesSubject.next(data),
      error: (err) => console.error('Error loading categories:', err)
    });

  }

  // Fetches the tree (the backend now populates the 'children' property)
  getCategoryTree(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.categoriesUrl}/tree`);
  }

  createCategory(category: Category): Observable<Category> {
    return this.http.post<Category>(this.categoriesUrl, category);
  }

  get categories(): Observable<Category[]> {
    return this.categories$;
  }
}