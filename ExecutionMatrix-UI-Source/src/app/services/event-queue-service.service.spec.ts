import { TestBed } from '@angular/core/testing';

import { EventQueueServiceService } from './event-queue-service.service';

describe('EventQueueServiceService', () => {
  let service: EventQueueServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EventQueueServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
