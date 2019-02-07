import { TestBed } from '@angular/core/testing';

import { ApplicationListService } from './application-list.service';

describe('ApplicationListService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ApplicationListService = TestBed.get(ApplicationListService);
    expect(service).toBeTruthy();
  });
});
