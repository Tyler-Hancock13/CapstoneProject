import { Component, OnInit } from '@angular/core';
import { Employee } from '../models/employee';
import { EmployeeService } from '../services/employee.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css'],
})
export class EmployeesComponent {
  /**
   * @property {string} searchText - The text the user enters into the search box.
   */
  searchText: string = '';

  /**
   * @property {Array<Employee>} employees - Stores result of multiple employees for a search by last name.
   */
  employees: Array<Employee>;

  /**
   * @property {string} employee - Stores result of single employee from search by id.
   */
  employee: Employee;

  /**
   * @property {string} message - A message to be displayed to the user.
   */
  message: string;

  /**
   * @property {boolean} loading - Flag to indicate if the app is waiting for results from an API Call.
   */
  loading: boolean;

  /**
   * @constructor
   * @param {EmployeeService} service - The service for Employees.
   */
  constructor(private service: EmployeeService) {}

  /**
   * Method for form submit.
   */
  search() {
    this.clear();

    if (this.searchText != '') {
      if (isNaN(Number(this.searchText))) {
        this.loading = true;

        this.service
          .search(this.searchText)
          .subscribe(
            (employees) => {
              this.employees = employees;
            },
            (err) => {
              this.message = err;
            }
          )
          .add(() => {
            this.loading = false;

            if (!this.employees || this.employees.length == 0) {
              this.message = 'The search returned no results.';
            }
          });
      } else {
        this.getEmployee(this.searchText);
      }
    } else {
      this.message = 'Please input a search criteria.';
    }

    this.searchText = '';
  }

  /**
   * Retrieves an employee by id.
   * @param {string} id - The ID of the employee to retrieve.
   */
  private getEmployee(id: string) {
    this.loading = true;

    this.service
      .get(id)
      .subscribe(
        (employee) => {
          this.employee = employee;
        },
        (err) => {
          this.handleError(err);
        }
      )
      .add(() => {
        this.loading = false;
        if (!this.employee) {
          this.message = 'The search returned no results.';
        }
      });
  }

  /**
   * Displays the detailed view for an employee.
   * @param {Employee} employee - The employee to display.
   */
  showDetails(employee: Employee) {
    this.clear();
    this.employee = employee;
  }

  /**
   * Clears all Data
   */
  private clear() {
    this.message = null;
    this.employees = null;
    this.employee = null;
  }

  /**
   * Handles errors from API
   * @param {HttpErrorResponse} error - The Error thrown by the API.
   */
  handleError(error: HttpErrorResponse) {
    switch (error.status) {
      case 404:
        this.message = 'The search returned no results.';
        break;
    }
  }
}
