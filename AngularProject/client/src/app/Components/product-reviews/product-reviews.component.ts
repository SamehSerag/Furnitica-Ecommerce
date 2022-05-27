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
  reviewExist: boolean = false;
  @Output() ReviewCount = new EventEmitter<number>();

  constructor(private reviewService: ReviewsService) { }

  ngOnInit(): void {
    this.GetReviews();
    this.GetUserReview();
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
          if (review != {})
            this.reviewExist=true;
        });
  }

  SubmitReview() {
    if (this.reviewExist) {
      this.PutReview();
    } else {
      this.PostReview();
    }
  }
  PostReview() {
    console.log("Post");
    console.log(this.userReview);
    this.userReview.productId=this.ProductId;
    this.reviewService.postReview(this.userReview).subscribe();
  }
  PutReview() {
    console.log("Put");
    this.reviewService.putReview(this.userReview).subscribe();
  }
}



