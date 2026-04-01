import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { Category } from '../../models/Category.model';
import { CategoryService } from '../../services/categoryService';

@Component({
  selector: 'app-category-filter',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './category-filter.component.html',
  styleUrls: ['./category-filter.component.css']
})
export class CategoryFilterComponent {
  categories$: Observable<Category[]>;
  @Output() select = new EventEmitter<string | undefined>();

  constructor(private categoryService: CategoryService) {
    this.categories$ = this.categoryService.categories;
  }

  onChange(event: Event) {
    const value = (event.target as HTMLSelectElement | null)?.value;
    this.select.emit(value || undefined);
  }
}
