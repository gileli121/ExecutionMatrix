import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompFeaturesAllVersionsComponent } from './comp-features-all-versions.component';

describe('CompFeaturesAllVersionsComponent', () => {
  let component: CompFeaturesAllVersionsComponent;
  let fixture: ComponentFixture<CompFeaturesAllVersionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompFeaturesAllVersionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompFeaturesAllVersionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
