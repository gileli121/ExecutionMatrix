import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompPageMessageComponent } from './comp-page-message.component';

describe('CompPageMessageComponent', () => {
  let component: CompPageMessageComponent;
  let fixture: ComponentFixture<CompPageMessageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompPageMessageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CompPageMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
