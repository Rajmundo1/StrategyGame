import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CountyPageComponent } from './county-page.component';

describe('CountyPageComponent', () => {
  let component: CountyPageComponent;
  let fixture: ComponentFixture<CountyPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CountyPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CountyPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
