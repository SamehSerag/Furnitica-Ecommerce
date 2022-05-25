import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IReview } from 'src/app/Models/ireviews';
import { ReviewsService } from 'src/app/Services/reviews.service';

@Component({
  selector: 'app-product-reviews',
  templateUrl: './product-reviews.component.html',
  styleUrls: ['./product-reviews.component.css']
})
export class ProductReviewsComponent implements OnInit {
  @Input() ProductId?: number = 0;
  reviews: IReview[] = [];
  userReview: IReview = <IReview>{};
  newReview: IReview = <IReview>{};
  @Output() ReviewCount = new EventEmitter<number>();

  constructor(private reviewService: ReviewsService) { }

  ngOnInit(): void {
    this.GetReviews();
    this.GetUserReview();

    console.log(this.reviews);
  }

  GetReviews() {
    if (this.ProductId)
      this.reviewService.getAllReviews(this.ProductId)
        .subscribe(reviews => {
          this.reviews = reviews.data;
          this.ReviewCount.emit(this.reviews.length);
        });
  }
  GetUserReview() {
    if (this.ProductId)
      this.reviewService.getUserReview(this.ProductId)
        .subscribe(review => {
          this.userReview = review;
        });
  }

  SubmitReview() {
console.log("Reviews");
console.log(this.newReview);
console.log(this.userReview);

   }
  PostReview() { }
  PutReview() { }

  numSequence(n: number): Array<number> {
    return new Array(n);
  }
}



