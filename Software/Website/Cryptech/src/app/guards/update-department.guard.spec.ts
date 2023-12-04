import { TestBed } from '@angular/core/testing';

import { UpdateDepartmentGuard } from './update-department.guard';

describe('UpdateDepartmentGuard', () => {
  let guard: UpdateDepartmentGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(UpdateDepartmentGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
