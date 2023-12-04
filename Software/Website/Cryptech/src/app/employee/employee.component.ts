import { Component, OnInit, Input } from '@angular/core';
import { Employee } from '../models/employee';

@Component({
  selector: 'app-employee',
  host: { class: 'card bg-primary text-dark' },
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css'],
})
export class EmployeeComponent {
  /**
   * @property {Employee} employee - The employee to be displayed.
   */
  @Input() employee: Employee;
}
