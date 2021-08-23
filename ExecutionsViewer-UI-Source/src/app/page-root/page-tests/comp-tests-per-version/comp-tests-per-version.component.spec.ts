import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompTestsPerVersionComponent } from './comp-tests-per-version.component';

describe('CompTestsPerVersionComponent', () => {
  let component: CompTestsPerVersionComponent;
  let fixture: ComponentFixture<CompTestsPerVersionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompTestsPerVersionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompTestsPerVersionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
