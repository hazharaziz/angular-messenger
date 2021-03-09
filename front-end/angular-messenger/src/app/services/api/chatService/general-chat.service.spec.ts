import { TestBed } from '@angular/core/testing';

import { GeneralChatService } from './general-chat.service';

describe('GeneralChatService', () => {
  let service: GeneralChatService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GeneralChatService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
