import { TestBed } from '@angular/core/testing';

import { ProjecttaskService } from './projecttask.service';

describe('ProjecttaskService', () => {
  let service: ProjecttaskService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProjecttaskService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
