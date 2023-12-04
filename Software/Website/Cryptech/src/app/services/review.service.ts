import { Injectable } from '@angular/core';
import { SharedService, API_URL } from './shared.service';
import { HttpClient } from '@angular/common/http';
import { GlobalService } from './global.service';
import { Review } from '../models/review';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Employee } from '../models/employee';

@Injectable({
  providedIn: 'root',
})
export class ReviewService extends SharedService {
  /**
   * @constructor
   * @param {HttpClient} http - Sends http requests
   * @param {GlobalService} globals - Service for storing global variables
   */
  constructor(private http: HttpClient, public globals: GlobalService) {
    super(globals);
  }

  /**
   * Creates a review.
   * @param review - The Review to be created.
   * @returns - The review that was created.
   */
  public create(review: Review): Observable<Review> {
    const API_METHOD = `${API_URL}/reviews/create`;
    return this.http
      .post<Review>(API_METHOD, review, this.httpOptions())
      .pipe(catchError(this.handleError));
  }

  /**
   * Gets the employees of a supervisor who are pending review.
   * @param {string} supervisorId - The id of the supervisor who's pending employees are to be retrieved.
   * @returns - The supervisors pending employees.
   */
  public getPendingEmployees(supervisorId: string): Observable<Employee[]> {
    const API_METHOD = `${API_URL}/reviews/pending/${supervisorId}`;
    return this.http
      .get<Employee[]>(API_METHOD, this.httpOptions())
      .pipe(catchError(this.handleError));
  }

  /**
   * Gets the reviews for a particular employee.
   * @param {string} employeeId - The id of the employee who's reviews are to be retrieved.
   * @returns - The employees reviews.
   */
  public getByEmployee(employeeId: string): Observable<Review[]> {
    const API_METHOD = `${API_URL}/reviews/${employeeId}`;
    return this.http
      .get<Review[]>(API_METHOD, this.httpOptions())
      .pipe(catchError(this.handleError));
  }
}
