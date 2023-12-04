import { Error } from './error';

/**
 * @class Base
 */
export abstract class Base {
  /**
   * @property {Array<Error>} errors - A collection to store the models errors.
   */
  errors: Array<Error>;

  /**
   * Determines if a model has errors.
   * @returns boolean
   */
  isValid(): boolean {
    return this.errors.length == 0;
  }
}
