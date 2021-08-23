import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompMainFeaturesPerVersionComponent } from './comp-main-features-per-version.component';

describe('CompMainFeaturesPerVersionComponent', () => {
  let component: CompMainFeaturesPerVersionComponent;
  let fixture: ComponentFixture<CompMainFeaturesPerVersionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompMainFeaturesPerVersionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompMainFeaturesPerVersionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
