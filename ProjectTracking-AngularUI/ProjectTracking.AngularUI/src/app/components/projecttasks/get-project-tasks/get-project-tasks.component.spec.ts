import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetProjectTasksComponent } from './get-project-tasks.component';

describe('GetProjectTasksComponent', () => {
  let component: GetProjectTasksComponent;
  let fixture: ComponentFixture<GetProjectTasksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GetProjectTasksComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetProjectTasksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
