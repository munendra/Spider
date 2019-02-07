import { TestBed } from '@angular/core/testing';

import { EnvironmentAddService } from './environment-add.service';

describe('EnvironmentAddService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EnvironmentAddService = TestBed.get(EnvironmentAddService);
    expect(service).toBeTruthy();
  });
});
