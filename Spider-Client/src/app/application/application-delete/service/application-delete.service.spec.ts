import { TestBed } from '@angular/core/testing';

import { ApplicationDeleteService } from './application-delete.service';

describe('ApplicationDeleteService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ApplicationDeleteService = TestBed.get(ApplicationDeleteService);
    expect(service).toBeTruthy();
  });
});
