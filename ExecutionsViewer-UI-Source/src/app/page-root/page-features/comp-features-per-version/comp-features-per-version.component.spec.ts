import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompFeaturesPerVersionComponent } from './comp-features-per-version.component';

describe('CompFeaturesPerVersionComponent', () => {
  let component: CompFeaturesPerVersionComponent;
  let fixture: ComponentFixture<CompFeaturesPerVersionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompFeaturesPerVersionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompFeaturesPerVersionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
