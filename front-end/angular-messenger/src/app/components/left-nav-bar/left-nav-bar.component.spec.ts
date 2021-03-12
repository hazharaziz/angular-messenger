import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeftNavBarComponent } from './left-nav-bar.component';

describe('LeftNavBarComponent', () => {
  let component: LeftNavBarComponent;
  let fixture: ComponentFixture<LeftNavBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeftNavBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeftNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
