import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompConsoleShowDiffsDialogComponent } from './comp-console-show-diffs-dialog.component';

describe('CompConsoleShowDiffsDialogComponent', () => {
  let component: CompConsoleShowDiffsDialogComponent;
  let fixture: ComponentFixture<CompConsoleShowDiffsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompConsoleShowDiffsDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompConsoleShowDiffsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
