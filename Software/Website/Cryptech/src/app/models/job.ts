import { Base } from './base';
import { Department } from './department';

/**
 * Job Class
 * @class
 * @extends Base
 */
export class Job extends Base {
  /**
   * @property {number} id - The Jobs ID
   */
  id: number;

  /**
   * @property {string} name - The name of the Job.
   */
  name: string;
}
