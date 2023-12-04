import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalService } from '../services/global.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Review } from '../models/review';
import { Employee } from '../models/employee';
import { EmployeeService } from '../services/employee.service';
import { ReviewService } from '../services/review.service';

@Component({
  selector: 'app-review-form',
  templateUrl: './review-form.component.html',
  styleUrls: ['./review-form.component.css'],
})
export class ReviewFormComponent implements OnInit {
  /**
   * @property {Employee} supervisor - The supervisor creating the review.
   */
  supervisor: Employee;

  /**
   * @property {Review} review - The review created by the user.
   */
  review: Review = new Review();

  /**
   * @property {Employee[]} employees - The supervisors employees.
   */
  employees: Employee[];

  /**
   * @property {boolean} success - A flag indicating if the creation of a review was successful.
   */
  success: boolean = false;

  /**
   * @property {FormGroup} form - The form for collecting user-input.
   */
  form: FormGroup = new FormGroup({
    employeeID: new FormControl('', Validators.required),
    rating: new FormControl('', Validators.required),
    date: new FormControl('', Validators.required),
    comment: new FormControl('', Validators.required),
  });

  /**
   * @constructor
   * @param {ActivatedRoute} route - The route for retrieving route parameter values.
   * @param {GlobalService} globals - Service for storing global variables.
   * @param {EmployeeService} employeeService - Service for Employees.
   * @param {ReviewService} reviewService - Service for Reviews.
   */
  constructor(
    private route: ActivatedRoute,
    private globals: GlobalService,
    private employeeService: EmployeeService,
    private reviewService: ReviewService
  ) {}

  /**
   * Retrieving the supervisor and their employees.
   */
  ngOnInit(): void {
    let supervisorId = this.route.snapshot.paramMap.get('supervisorId');
    let employeeId = this.route.snapshot.paramMap.get('employeeId');

    if (employeeId != null) {
      this.form.patchValue({
        employeeID: employeeId,
      });
    }

    this.employeeService.get(supervisorId).subscribe((e) => {
      this.supervisor = e;
    });

    this.employeeService.getBySupervisor(supervisorId).subscribe((e) => {
      this.employees = e;
    });
  }

  /**
   * Attempts to create a review.
   */
  create() {
    this.review = this.form.value;
    this.review.supervisorID = this.supervisor.id;
    this.success = false;

    if (this.form.invalid) return;

    this.reviewService.create(this.review).subscribe(
      (review) => {
        this.review = review;

        if (this.review.errors.length == 0) {
          this.success = true;
          this.form.reset();
          this.form.markAsPristine();
        }
      },
      (error) => {
        console.error(error);
      }
    );
  }

  /**
   * Resets the success flag.
   */
  formChanged() {
    this.success = false;
  }

  /**
   * Getter for employee control.
   */
  get employeeID() {
    return this.form.get('employeeID');
  }

  /**
   * Getter for rating control.
   */
  get rating() {
    return this.form.get('rating');
  }

  /**
   * Getter for date control.
   */
  get date() {
    return this.form.get('date');
  }

  /**
   * Getter for comment control.
   */
  get comment() {
    return this.form.get('comment');
  }
}
