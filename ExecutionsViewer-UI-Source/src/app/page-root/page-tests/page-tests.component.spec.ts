import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageTestsComponent } from './page-tests.component';

describe('PageTestsComponent', () => {
  let component: PageTestsComponent;
  let fixture: ComponentFixture<PageTestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PageTestsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PageTestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
