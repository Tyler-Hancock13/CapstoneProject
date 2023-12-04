import { Component, OnInit, Input } from '@angular/core';
import { Employee } from '../models/employee';

@Component({
  selector: 'app-employee-details',
  templateUrl: './employee-details.component.html',
  styleUrls: ['./employee-details.component.css'],
})
export class EmployeeDetailsComponent {
  /**
   * @property {Employee} employee - The employee to display the details of.
   */
  @Input() employee: Employee;
}
