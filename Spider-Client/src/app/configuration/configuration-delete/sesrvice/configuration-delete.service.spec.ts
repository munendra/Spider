import { TestBed } from '@angular/core/testing';

import { ConfigurationDeleteService } from './configuration-delete.service';

describe('EnvironmentListService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ConfigurationDeleteService = TestBed.get(ConfigurationDeleteService);
    expect(service).toBeTruthy();
  });
});
