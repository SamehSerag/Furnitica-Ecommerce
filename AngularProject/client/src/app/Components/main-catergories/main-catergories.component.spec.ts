import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainCatergoriesComponent } from './main-catergories.component';

describe('MainCatergoriesComponent', () => {
  let component: MainCatergoriesComponent;
  let fixture: ComponentFixture<MainCatergoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MainCatergoriesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MainCatergoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
