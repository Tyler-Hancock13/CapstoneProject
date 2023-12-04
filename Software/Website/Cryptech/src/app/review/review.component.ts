import { Component, OnInit, Input } from '@angular/core';
import { Review } from '../models/review';

@Component({
  selector: 'app-review',
  host: { class: 'card bg-primary text-dark' },
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css'],
})
export class ReviewComponent implements OnInit {
  /**
   * @property {Review} review - The review to be displayed.
   */
  @Input() review: Review;

  /**
   * @property {number} quarter - The quarter in which the review was created.
   */
  quarter: number;

  /**
   * @property {string} year - The year the performance review was created for.
   */
  year: string;

  constructor() {}

  ngOnInit(): void {
    this.quarter =
      (Math.ceil((new Date(this.review.date).getMonth() + 1) / 3) + 3) % 4 == 0
        ? 4
        : (Math.ceil((new Date(this.review.date).getMonth() + 1) / 3) + 3) % 4;

    this.year =
      this.quarter == 4
        ? (new Date(this.review.date).getFullYear() - 1).toString()
        : new Date(this.review.date).getFullYear().toString();
  }
}
