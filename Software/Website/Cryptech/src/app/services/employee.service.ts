import { Injectable } from '@angular/core';
import { SharedService, API_URL } from './shared.service';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable, of, throwError } from 'rxjs';
import { Employee } from '../models/employee';
import { GlobalService } from './global.service';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService extends SharedService {
  /**
   * @constructor
   * @param {HttpClient} http - Sends http requests
   * @param {GlobalService} globals - Service for storing global variables
   */
  constructor(private http: HttpClient, globals: GlobalService) {
    super(globals);
  }

  /**
   * Gets an Employee by ID
   * @param {string} id - The ID of the Employee to retrieve.
   * @returns {Observable<Employee>}
   */
  get(id: string): Observable<Employee> {
    const API_METHOD = `${API_URL}/employees/${id}`;
    return this.http
      .get<Employee>(API_METHOD)
      .pipe(catchError(this.handleError));
  }

  /**
   * Retrieves a list of Employees by full or partial last name.
   * @param {string} lastName - The last name to search for.
   * @returns {Observable<Employee[]>}
   */
  search(lastName: string): Observable<Employee[]> {
    const API_METHOD = `${API_URL}/employees/search/${lastName}`;
    return this.http
      .get<Employee[]>(API_METHOD)
      .pipe(catchError(this.handleError));
  }

  /**
   * Retrieves a set of Employees overseen by a particular supervisor.
   * @param supervisorId - The ID of the Supervisor who's Employees are to be retrieved.
   * @returns - The Employees overseen by the Supervisor.
   */
  getBySupervisor(supervisorId: string): Observable<Employee[]> {
    const API_METHOD = `${API_URL}/employees/supervisor/${supervisorId}`;
    return this.http
      .get<Employee[]>(API_METHOD)
      .pipe(catchError(this.handleError));
  }

  /**
   * Updates an employee.
   * @param employee - The employe to be updated.
   * @returns - The updated employee.
   */
  update(employee: Employee): Observable<Employee> {
    const API_METHOD = `${API_URL}/employees/update`;
    return this.http
      .post<Employee>(API_METHOD, employee, this.httpOptions())
      .pipe(catchError(this.handleError));
  }
}
