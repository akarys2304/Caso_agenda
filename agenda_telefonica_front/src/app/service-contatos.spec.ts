import { TestBed } from '@angular/core/testing';

import { ServiceContatos } from './service-contatos';

describe('ServiceContatos', () => {
  let service: ServiceContatos;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServiceContatos);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
