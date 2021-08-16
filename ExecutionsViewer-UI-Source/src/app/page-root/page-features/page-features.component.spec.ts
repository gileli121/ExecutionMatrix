import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageFeaturesComponent } from './page-features.component';

describe('PageFeaturesComponent', () => {
  let component: PageFeaturesComponent;
  let fixture: ComponentFixture<PageFeaturesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PageFeaturesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PageFeaturesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
