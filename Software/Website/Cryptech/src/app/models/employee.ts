import { Base } from './base';

/**
 * Employee
 * @class
 * @extends Base
 */
export class Employee extends Base {
  /**
   * @property {string} id - The Employees ID.
   */
  id: string;

  /**
   * @property {string} firstName - The Employees first name.
   */
  firstName: string;

  /**
   * @property {string} lastName - The Employees last name.
   */
  lastName: string;

  /**
   * @property {string} middleInitial - The Employees middle initial.
   */
  middleInitial: string;

  /**
   * @property {string} fullName - The Employees full name.
   */
  fullName: string;

  /**
   * @property {Date} dateOfBirth - The Employees date of birth.
   */
  dateOfBirth: Date;

  /**
   * @property {string} streetAddress - The Employees street address.
   */
  streetAddress: string;

  /**
   * @property {string} city - The Employees city.
   */
  city: string;

  /**
   * @property {string} postalCode - The Employees postal code.
   */
  postalCode: string;

  /**
   * @property {string} sin - The Employees Social Insurance Number.
   */
  sin: string;

  /**
   * @property {Date} seniorityDate - The Date the Employee started with the company.
   */
  seniorityDate: Date;

  /**
   * @property {Date} jobStartDate - The Date the Employee started their current job.
   */
  jobStartDate: Date;

  /**
   * @property {string} workPhone - The Employees work phone number.
   */
  workPhone: string;

  /**
   * @property {string} cellPhone - The Employees cell phone number.
   */
  cellPhone: string;

  /**
   * @property {string} emailAddress - The Employees email address.
   */
  emailAddress: string;

  /**
   * @property {string} version - The version of the record.
   */
  version: string;

  /**
   * @property {number} jobID - The ID of the Employees Job.
   */
  jobID: number;

  /**
   * @property {number} supervisorID - The ID of the Employees Supervisor.
   */
  supervisorID: string;

  /**
   * @property {number} departmentID - The ID of the Employees Department.
   */
  departmentID: number;
}
