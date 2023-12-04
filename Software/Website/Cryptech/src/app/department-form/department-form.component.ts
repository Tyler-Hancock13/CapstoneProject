import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Department } from '../models/department';
import { DepartmentService } from '../services/department.service';
import { AuthenticationService } from '../services/authentication.service';
import { User } from '../models/user';
import { Employee } from '../models/employee';
import { EmployeeService } from '../services/employee.service';
import { Role } from '../models/role.enum';

@Component({
  selector: 'app-department-form',
  templateUrl: './department-form.component.html',
  styleUrls: ['./department-form.component.css'],
})
export class DepartmentFormComponent implements OnInit {
  /**
   * @property {Department[]} departments - All existing departments.
   */
  departments: Department[];

  /**
   * @property {Department} department - The department to be updated.
   */
  department: Department = new Department();

  /**
   * @property {boolean} success - Flag indicating if the update was successful or not.
   */
  success: boolean = false;

  /**
   * @property {User} user - The logged-in user of the application.
   */
  user: User = new User();

  /**
   * @property {Employee} employee - The employee that owns the logged-in user account.
   */
  employee: Employee;

  /**
   * @property {FormGroup} form - The form for collecting user-input.
   */
  form: FormGroup = new FormGroup({
    id: new FormControl('', Validators.required),
    name: new FormControl(''),
    description: new FormControl(''),
    invocationDate: new FormControl(''),
  });

  /**
   * @constructor
   * @param {DepartmentService} departmentService - The service for departments.
   * @param {AuthenticationService} authenticationService - The service for authentication.
   * @param {EmployeeService} employeeService - The service for employees,
   */
  constructor(
    private departmentService: DepartmentService,
    private authenticationService: AuthenticationService,
    private employeeService: EmployeeService
  ) {}

  /**
   * Getting departments and user, if the user is a supervisor selecting their department.
   */
  ngOnInit(): void {
    this.getDepartments();
    this.user = this.authenticationService.globals.user;

    if (this.user.role == Role.Supervisor) {
      this.employeeService.get(this.user.employeeID).subscribe(
        (employee) => {
          this.employee = employee;
          this.form.patchValue({
            id: employee.departmentID,
          });
          this.onDepartmentSelected();
        },
        (error) => {
          console.error(error);
        }
      );
    }
  }

  /**
   * Getting all departments.
   */
  private getDepartments() {
    this.departmentService.getAll().subscribe((departments) => {
      this.departments = departments;
    });
  }

  /**
   * Attempts to update the department.
   */
  update() {
    this.success = false;

    if (this.form.invalid) return;
    let version = this.department.version;
    this.department = this.form.value;
    this.department.version = version;

    this.departmentService.update(this.department).subscribe(
      (d) => {
        this.department = d;
        if (d.errors.length == 0) {
          if (this.user.role != Role.Supervisor) {
            this.form.reset();
          }

          this.form.markAsPristine();
          this.success = true;
        }
      },
      (e) => {
        console.error(e);
      }
    );

    this.getDepartments();
    this.onDepartmentSelected();
  }

  /**
   * Storing the selected department and populating the form with it's info.
   */
  onDepartmentSelected() {
    this.department = this.departments.find(
      (d) => d.id == this.form.controls.id.value
    );

    this.form.patchValue({
      name: this.department.name,
      description: this.department.description,
      invocationDate: this.formatDate(this.department.invocationDate),
    });
  }

  /**
   * Formats a date to be displayed in a date picker.
   * @param {Date} date - The date to be formatted.
   */
  private formatDate(date: Date): string {
    const d = new Date(date);
    let month = '' + (d.getMonth() + 1);
    let day = '' + d.getDate();
    const year = d.getFullYear();
    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;
    return [year, month, day].join('-');
  }

  /**
   * Getter for the department dropdown.
   */
  get id() {
    return this.form.get('id');
  }
}
