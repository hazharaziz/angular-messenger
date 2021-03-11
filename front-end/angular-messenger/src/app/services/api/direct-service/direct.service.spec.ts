import { TestBed } from '@angular/core/testing';

import { DirectService } from './direct.service';

describe('DirectService', () => {
  let service: DirectService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DirectService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
