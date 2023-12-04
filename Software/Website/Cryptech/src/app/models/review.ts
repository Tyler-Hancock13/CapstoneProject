import { Rating } from './rating.enum';
import { Base } from './base';

/**
 * Review
 * @class
 * @extends Base
 */
export class Review extends Base {
  /**
   * @property {number} id - The id of the review.
   */
  id: number;

  /**
   * @property {string} employeeID - The id of the employee the review was created for.
   */
  employeeID: string;

  /**
   * @property {string} supervisorID - The id of the supervisor that created the review.
   */
  supervisorID: string;

  /**
   * @property {Rating} rating - The rating the employee recieved on the review.
   */
  rating: Rating;

  /**
   * @property {string} comment - The detailed comment for the review.
   */
  comment: string;

  /**
   * @property {Date} date - The date the review was created.
   */
  date: Date;
}
