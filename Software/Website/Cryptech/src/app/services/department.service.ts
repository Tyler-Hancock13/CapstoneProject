import { Injectable } from '@angular/core';
import { SharedService, API_URL } from './shared.service';
import { HttpClient } from '@angular/common/http';
import { GlobalService } from './global.service';
import { Department } from '../models/department';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService extends SharedService {
  /**
   * @constructor
   * @param {HttpClient} http - Sends http requests
   * @param {GlobalService} globals - Service for storing global variables
   */
  constructor(private http: HttpClient, public globals: GlobalService) {
    super(globals);
  }

  /**
   * Updates a department.
   * @param department - The department to be updated.
   * @returns - The updated department.
   */
  public update(department: Department): Observable<Department> {
    const API_METHOD = `${API_URL}/departments/update`;
    return this.http
      .post<Department>(API_METHOD, department, this.httpOptions())
      .pipe(catchError(this.handleError));
  }

  /**
   * Gets all departments.
   * @returns - The departments.
   */
  public getAll(): Observable<Department[]> {
    const API_METHOD = `${API_URL}/departments`;
    return this.http
      .get<Department[]>(API_METHOD, this.httpOptions())
      .pipe(catchError(this.handleError));
  }
}
