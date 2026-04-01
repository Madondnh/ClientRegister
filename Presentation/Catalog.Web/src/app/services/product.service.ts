import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Category } from '../models/Category.model';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = `${environment.apiUrl}/products`;
  private filteredProductsApiUrl = `${environment.apiUrl}/products/Category`;
  private searchProductsApiUrl = `${environment.apiUrl}/products/Search`;

  constructor(private http: HttpClient) {
  }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }

  getFilterdProducts(categoryId: string): Observable<Product[]> {
    // We use HttpParams to pass the ID as a query string (e.g., ?categoryId=123)
    const params = { categoryId: categoryId };

    return this.http.get<Product[]>(`${this.filteredProductsApiUrl}/${categoryId}`);
  }

  getById(id: string): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }

  updateProduct(product: Product): Observable<Product> {
    return this.http.put<Product>(`${this.apiUrl}/${product.id}`, product);
  }

  deleteProduct(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  search(term: string): Observable<Product[]> {
    const searchKey = term
      .trim()
      .split(/\s+/)
      .map(word => word.trim())
      .join('+');

    const options = {
      params: new HttpParams().set('searchKey', searchKey)
    };
    
    return this.http.get<Product[]>(this.searchProductsApiUrl, options);
  }
}
