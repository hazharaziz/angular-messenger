import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DirectItemComponent } from './direct-item.component';

describe('DirectItemComponent', () => {
  let component: DirectItemComponent;
  let fixture: ComponentFixture<DirectItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DirectItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DirectItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
