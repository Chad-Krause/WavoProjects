import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditTeamsComponent } from './edit-teams.component';

describe('EditTeamsComponent', () => {
  let component: EditTeamsComponent;
  let fixture: ComponentFixture<EditTeamsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditTeamsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditTeamsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
