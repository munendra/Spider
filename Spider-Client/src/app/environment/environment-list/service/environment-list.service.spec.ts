import { TestBed } from '@angular/core/testing';

import { EnvironmentListService } from './environment-list.service';

describe('EnvironmentListService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EnvironmentListService = TestBed.get(EnvironmentListService);
    expect(service).toBeTruthy();
  });
});
