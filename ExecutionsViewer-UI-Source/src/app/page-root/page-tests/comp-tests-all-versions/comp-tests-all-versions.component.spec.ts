import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompTestsAllVersionsComponent } from './comp-tests-all-versions.component';

describe('CompTestsAllVersionsComponent', () => {
  let component: CompTestsAllVersionsComponent;
  let fixture: ComponentFixture<CompTestsAllVersionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompTestsAllVersionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompTestsAllVersionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
