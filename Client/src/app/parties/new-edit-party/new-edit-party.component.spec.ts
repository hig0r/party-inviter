import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewEditPartyComponent } from './new-edit-party.component';

describe('NewEditPartyComponent', () => {
  let component: NewEditPartyComponent;
  let fixture: ComponentFixture<NewEditPartyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewEditPartyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewEditPartyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
