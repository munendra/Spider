import { TestBed } from '@angular/core/testing';

import { ApplicationEditService } from './application-edit.service';

describe('ApplicationEditService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ApplicationEditService = TestBed.get(ApplicationEditService);
    expect(service).toBeTruthy();
  });
});
