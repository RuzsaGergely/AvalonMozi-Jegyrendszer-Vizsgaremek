import { TestBed } from '@angular/core/testing';
import { CartHandler } from './carthandler.service';


describe('CartHandlerService', () => {
  let service: CartHandler;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CartHandler);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});