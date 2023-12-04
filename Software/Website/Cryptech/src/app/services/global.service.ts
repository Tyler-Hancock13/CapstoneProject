import { Injectable } from '@angular/core';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class GlobalService {
  /**
   * @property {User} user - The logged in user.
   */
  public user: User;
}
