import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { debounceTime, Subject } from 'rxjs';

@Component({
  selector: 'app-search-bar',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent {
  private term$ = new Subject<string>();
  @Output() search = new EventEmitter<string>();

  constructor() {
    this.term$.pipe(debounceTime(250)).subscribe(t => this.search.emit(t));
  }

  onInput(event: Event) {
    const value = (event.target as HTMLInputElement).value;
    this.term$.next(value.trim());
  }
}
