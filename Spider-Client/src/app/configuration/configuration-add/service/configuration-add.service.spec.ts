import { TestBed } from '@angular/core/testing';

import { ConfigurationAddService } from './configuration-add.service';

describe('ConfigurationAddService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ConfigurationAddService = TestBed.get(ConfigurationAddService);
    expect(service).toBeTruthy();
  });
});
