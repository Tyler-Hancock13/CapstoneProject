import { Base } from './base';
import { Job } from './job';

/**
 * Department
 * @class
 * @extends Base
 */
export class Department extends Base {
  /**
   * @property {number} id = The Departments ID.
   */
  id: number;

  /**
   * @property {string} name - The Departments name.
   */
  name: string;

  /**
   * @property {string} description - A description of the Department.
   */
  description: string;

  /**
   * @property {Date} invocationDate - The Date the department did or will become active.
   */
  invocationDate: Date;

  /**
   * @property {string} version - The version of the record.
   */
  version: string;
}
