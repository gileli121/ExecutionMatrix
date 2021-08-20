import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageMainFeaturesComponent } from './page-main-features.component';

describe('PageMainFeaturesComponent', () => {
  let component: PageMainFeaturesComponent;
  let fixture: ComponentFixture<PageMainFeaturesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PageMainFeaturesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PageMainFeaturesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
