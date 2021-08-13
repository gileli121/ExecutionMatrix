import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompVersionSelectorComponent } from './comp-version-selector.component';

describe('CompVersionSelectorComponent', () => {
  let component: CompVersionSelectorComponent;
  let fixture: ComponentFixture<CompVersionSelectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompVersionSelectorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompVersionSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
