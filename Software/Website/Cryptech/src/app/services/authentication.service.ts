import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { API_URL, SharedService } from './shared.service';
import { catchError } from 'rxjs/operators';
import { GlobalService } from './global.service';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService extends SharedService {
  /**
   * @constructor
   * @param {HttpClient} http - Used to produce http requests.
   * @param {GlobalService} globals - Service to store global variables.
   */
  constructor(private http: HttpClient, globals: GlobalService) {
    super(globals);
  }

  /**
   * Logs the user in.
   * @param {User} user - The User model to attempt login with.
   * @returns {User}
   */
  public login(user: User): Observable<User> {
    const API_METHOD = `${API_URL}/authentication/login`;
    return this.http
      .post<User>(API_METHOD, user, super.httpOptions())
      .pipe(catchError(super.handleError));
  }

  /**
   * Logs out the user.
   */
  public logout() {
    this.globals.user = null;
  }

  /**
   * Determines if a user has logged in or not.a
   * @returns {boolean}
   */
  public isLoggedIn(): boolean {
    return this.globals.user != null;
  }
}
