import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IReview } from '../Models/ireviews';

@Injectable({
  providedIn: 'root'
})

export class ReviewsService {

  constructor(private httpClient:HttpClient) { }

  getAllReviews() : Observable<IReview[]>{
    return this.httpClient.get<IReview[]>(`${environment.APIURL}/api/Reviews`)
  }

  getReviewById(id:number) :  Observable<IReview>
  {
    return this.httpClient.get<IReview>(`${environment.APIURL}/api/Reviews/${id}`)
  }

  postReview(review:IReview)
  {
  }

  putReview(review:IReview)
  {

  }
}
