import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DirectsComponent } from './directs.component';

describe('DirectsComponent', () => {
  let component: DirectsComponent;
  let fixture: ComponentFixture<DirectsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DirectsComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DirectsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
