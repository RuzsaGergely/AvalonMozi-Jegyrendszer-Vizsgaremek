import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieBySeoTitle } from './movie-by-seotitle';

describe('MovieBySeoTitle', () => {
  let component: MovieBySeoTitle;
  let fixture: ComponentFixture<MovieBySeoTitle>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MovieBySeoTitle]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MovieBySeoTitle);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
