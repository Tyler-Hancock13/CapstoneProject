import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Review } from '../models/review';
import { EmployeeService } from '../services/employee.service';
import { Employee } from '../models/employee';
import { Rating } from '../models/rating.enum';

@Component({
  selector: 'app-review-details',
  host: { class: 'card mt-4 bg-primary text-dark pb-3 pt-3' },
  templateUrl: './review-details.component.html',
  styleUrls: ['./review-details.component.css'],
})
export class ReviewDetailsComponent implements OnInit {
  /**
   * @property {Review} review - The review to be displayed.
   */
  @Input() review: Review;

  /**
   * @property {EventEmitter<any>} onBackClicked - Event Emitter for handling back button press.
   */
  @Output() onBackClicked: EventEmitter<any> = new EventEmitter();

  /**
   * @property {Employee} supervisor - The supervisor that created the review.
   */
  supervisor: Employee;

  /**
   * @property {number} quarter - The quarter in which the review was created.
   */
  quarter: number;

  /**
   * @property {string} rating - The reviews rating converted to a friendly string.
   */
  rating: string;

  /**
   * @property {string} year - The year the performance review was created for.
   */
  year: string;

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.employeeService.get(this.review.supervisorID).subscribe((s) => {
      this.supervisor = s;
    });

    this.quarter =
      (Math.ceil((new Date(this.review.date).getMonth() + 1) / 3) + 3) % 4 == 0
        ? 4
        : (Math.ceil((new Date(this.review.date).getMonth() + 1) / 3) + 3) % 4;

    this.year =
      this.quarter == 4
        ? (new Date(this.review.date).getFullYear() - 1).toString()
        : new Date(this.review.date).getFullYear().toString();

    switch (this.review.rating) {
      case Rating.BelowExpectations:
        this.rating = 'Below Expectations';
        break;
      case Rating.MeetsExpectations:
        this.rating = 'Meets Expectations';
        break;
      case Rating.ExceedsExpectations:
        this.rating = 'Exceeds Expectations';
        break;
    }
  }

  goBack() {
    this.onBackClicked.emit();
  }
}
