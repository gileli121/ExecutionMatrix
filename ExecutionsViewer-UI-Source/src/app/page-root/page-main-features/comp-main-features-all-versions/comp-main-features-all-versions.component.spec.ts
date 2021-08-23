import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompMainFeaturesAllVersionsComponent } from './comp-main-features-all-versions.component';

describe('CompMainFeaturesAllVersionsComponent', () => {
  let component: CompMainFeaturesAllVersionsComponent;
  let fixture: ComponentFixture<CompMainFeaturesAllVersionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompMainFeaturesAllVersionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompMainFeaturesAllVersionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
