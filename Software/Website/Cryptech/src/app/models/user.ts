import { Role } from './role.enum';
import { Base } from './base';

/**
 * User
 * @class
 * @extends Base
 */
export class User extends Base {
  /**
   * @property {number} id - The Users ID.
   */
  id: number;

  /**
   * @property {string} employeeId - The ID of the Employee who owns the account.
   */
  employeeID: string;

  /**
   * @property {string} password - The Users password.
   */
  password: string;

  /**
   * @property {Role} role - The Role of the user within the company.
   */
  role: Role;
}
