import { TestBed } from '@angular/core/testing';

import { LanguagesChangeService } from './languages-change.service';

describe('LanguagesChangeService', () => {
  let service: LanguagesChangeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LanguagesChangeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
