import { TestBed } from '@angular/core/testing';

import { ApplicationAddService } from './application-add.service';

describe('ApplicationAddService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ApplicationAddService = TestBed.get(ApplicationAddService);
    expect(service).toBeTruthy();
  });
});
