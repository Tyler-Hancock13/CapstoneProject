import { ErrorType } from './error-type.enum';

/**
 * Error Class
 * @class
 */
export class Error {
  /**
   * @property {number} number - The Error Number
   */
  number: number;

  /**
   * @property {ErrorType} type - The type of Error.
   */
  type: ErrorType;

  /**
   * @property {string} message - The Error message.
   */
  message: string;
}
