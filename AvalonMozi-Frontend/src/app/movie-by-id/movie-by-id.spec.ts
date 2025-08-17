import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieById } from './movie-by-id';

describe('MovieById', () => {
  let component: MovieById;
  let fixture: ComponentFixture<MovieById>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MovieById]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MovieById);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
