import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitPageComponent } from './unit-page.component';

describe('UnitPageComponent', () => {
  let component: UnitPageComponent;
  let fixture: ComponentFixture<UnitPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnitPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnitPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
