import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminTicketcheck } from './admin-ticketcheck';

describe('AdminTicketcheck', () => {
  let component: AdminTicketcheck;
  let fixture: ComponentFixture<AdminTicketcheck>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminTicketcheck]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminTicketcheck);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
