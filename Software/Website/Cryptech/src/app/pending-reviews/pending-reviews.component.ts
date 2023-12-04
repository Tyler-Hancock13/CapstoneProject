import { Component, OnInit } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { ActivatedRoute } from '@angular/router';
import { Employee } from '../models/employee';
import { GlobalService } from '../services/global.service';

@Component({
  selector: 'app-pending-reviews',
  templateUrl: './pending-reviews.component.html',
  styleUrls: ['./pending-reviews.component.css'],
})
export class PendingReviewsComponent implements OnInit {
  employees: Employee[];
  supervisorId: string;

  constructor(
    private reviewService: ReviewService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.supervisorId = this.route.snapshot.paramMap.get('id');

    this.reviewService
      .getPendingEmployees(this.supervisorId)
      .subscribe((employees) => {
        this.employees = employees;
      });
  }
}
