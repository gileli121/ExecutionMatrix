import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompNavbar } from './comp-navbar.component';

describe('CompNavbarComponent', () => {
  let component: CompNavbar;
  let fixture: ComponentFixture<CompNavbar>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompNavbar ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompNavbar);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
