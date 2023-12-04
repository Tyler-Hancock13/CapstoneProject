import { Component, OnInit } from '@angular/core';
import { Review } from '../models/review';
import { ReviewService } from '../services/review.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css'],
})
export class ReviewsComponent implements OnInit {
  reviews: Review[];
  selectedReview: Review;

  constructor(
    private reviewService: ReviewService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    let employeeId = this.route.snapshot.paramMap.get('id');
    this.reviewService.getByEmployee(employeeId).subscribe((reviews) => {
      this.reviews = reviews;
    });
  }

  showDetails(review: Review) {
    this.selectedReview = review;
  }

  goBack() {
    this.selectedReview = null;
  }
}
