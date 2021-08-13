import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompShowTestExecutionComponent } from './comp-show-test-execution.component';

describe('CompShowTestExecutionComponent', () => {
  let component: CompShowTestExecutionComponent;
  let fixture: ComponentFixture<CompShowTestExecutionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompShowTestExecutionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompShowTestExecutionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
