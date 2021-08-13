import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompExecutionConsoleComponent } from './comp-execution-console.component';

describe('CompExecutionConsoleComponent', () => {
  let component: CompExecutionConsoleComponent;
  let fixture: ComponentFixture<CompExecutionConsoleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompExecutionConsoleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompExecutionConsoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
