import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { EmployeeService } from '../services/employee.service';
import { ActivatedRoute } from '@angular/router';
import { Employee } from '../models/employee';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.css'],
})
export class EmployeeFormComponent implements OnInit {
  /**
   * @property {Employee} employee - The Employee being updated.
   */
  employee: Employee = new Employee();

  /**
   * @property {boolean} success - Flag indicating if creation was successful.
   */
  success: boolean = false;

  /**
   * @property {FormGroup} form - The form for collecting user-input.
   */
  form: FormGroup = new FormGroup({
    streetAddress: new FormControl(''),
    city: new FormControl(''),
    postalCode: new FormControl(''),
    workPhone: new FormControl(''),
    cellPhone: new FormControl(''),
  });

  /**
   * @constructor
   * @param {EmployeeService} service - The service for Employees.
   * @param {ActivatedRoute} route - The route used for retrieving route parameters.
   */
  constructor(
    private service: EmployeeService,
    private route: ActivatedRoute
  ) {}

  /**
   * Getting Employee to update and displaying their data in the FormGroup.
   */
  ngOnInit(): void {
    let id: string = this.route.snapshot.paramMap.get('id');
    this.service.get(id).subscribe((e) => {
      this.employee = e;
      this.form.patchValue({
        streetAddress: this.employee.streetAddress,
        city: this.employee.city,
        postalCode: this.employee.postalCode,
        workPhone: this.employee.workPhone,
        cellPhone: this.employee.cellPhone,
      });
    });
  }

  /**
   * Attempts to update the Employee.
   */
  update() {
    this.success = false;

    if (this.form.invalid) return;

    this.employee.streetAddress = this.form.controls.streetAddress.value;
    this.employee.city = this.form.controls.city.value;
    this.employee.postalCode = this.form.controls.postalCode.value;
    this.employee.workPhone = this.form.controls.workPhone.value;
    this.employee.cellPhone = this.form.controls.cellPhone.value;

    this.service.update(this.employee).subscribe(
      (employee) => {
        this.employee = employee;

        if (employee.errors.length == 0) {
          this.form.markAsPristine();
          this.success = true;
        }
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
