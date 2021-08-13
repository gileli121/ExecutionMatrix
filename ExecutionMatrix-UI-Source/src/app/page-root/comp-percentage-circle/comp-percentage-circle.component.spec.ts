import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompPercentageCircleComponent } from './comp-percentage-circle.component';

describe('CompPercentageCircleComponent', () => {
  let component: CompPercentageCircleComponent;
  let fixture: ComponentFixture<CompPercentageCircleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompPercentageCircleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompPercentageCircleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
