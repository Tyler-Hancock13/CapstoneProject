import { throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { GlobalService } from './global.service';

export const API_URL = environment.apiUrl;

export class SharedService {
  /**
   * @constructor
   * @param {GlobalService} globals - Service to store global variables.
   */
  constructor(public globals: GlobalService) {}

  /**
   * Sets http headers.
   */
  protected httpOptions() {
    const HTTP_OPTIONS = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Accept: 'application/json',
      }),
    };
    return HTTP_OPTIONS;
  }

  /**
   * Sets http headers for login.
   */
  protected httpLoginOptions() {
    const HTTP_OPTIONS = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Accept: 'application/json',
        EmployeeID: this.globals.user.employeeID,
        Password: this.globals.user.password,
      }),
    };
    return HTTP_OPTIONS;
  }

  /**
   * Handles any errors with requests.
   */
  protected handleError(error: HttpErrorResponse) {
    return throwError(error);
  }
}
