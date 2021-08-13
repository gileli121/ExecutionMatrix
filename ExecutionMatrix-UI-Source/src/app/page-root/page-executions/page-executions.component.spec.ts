import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageExecutionsComponent } from './page-executions.component';

describe('PageExecutionsComponent', () => {
  let component: PageExecutionsComponent;
  let fixture: ComponentFixture<PageExecutionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PageExecutionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PageExecutionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
