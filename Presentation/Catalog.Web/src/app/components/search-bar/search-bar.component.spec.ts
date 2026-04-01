import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { SearchBarComponent } from './search-bar.component';

describe('SearchBarComponent', () => {
  let component: SearchBarComponent;
  let fixture: ComponentFixture<SearchBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SearchBarComponent] // Standalone components go in imports
    }).compileComponents();

    fixture = TestBed.createComponent(SearchBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should emit the search term after 250ms debounce', fakeAsync(() => {
    // 1. Spy on the EventEmitter
    spyOn(component.search, 'emit');

    // 2. Simulate user input
    const inputElement = fixture.nativeElement.querySelector('input');
    inputElement.value = 'laptop';
    inputElement.dispatchEvent(new Event('input'));

    // 3. Ensure it hasn't emitted immediately (because of debounce)
    expect(component.search.emit).not.toHaveBeenCalled();

    // 4. Fast-forward time by 250ms
    tick(250);

    // 5. Check if it was emitted with the correct value
    expect(component.search.emit).toHaveBeenCalledWith('laptop');
  }));

  it('should trim the search term', fakeAsync(() => {
    spyOn(component.search, 'emit');

    const inputElement = fixture.nativeElement.querySelector('input');
    inputElement.value = '  phone  ';
    inputElement.dispatchEvent(new Event('input'));

    tick(250);

    expect(component.search.emit).toHaveBeenCalledWith('phone');
  }));
  
  it('should allow multi-value input search', fakeAsync(() => {
    spyOn(component.search, 'emit');

    const inputElement = fixture.nativeElement.querySelector('input');
    inputElement.value = '  phone  number';
    inputElement.dispatchEvent(new Event('input'));

    tick(250);

    expect(component.search.emit).toHaveBeenCalledWith('phone  number');
  }));
});