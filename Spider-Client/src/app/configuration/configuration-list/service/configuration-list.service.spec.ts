import { TestBed } from '@angular/core/testing';

import { ConfigurationListService } from './configuration-list.service';

describe('EnvironmentListService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ConfigurationListService = TestBed.get(ConfigurationListService);
    expect(service).toBeTruthy();
  });
});
