import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {

  constructor(private httpClient: HttpClient) { }

  // getWishList(prdId: number): Observable<IReviewsPagination> {
  //   return this.httpClient.get<IReviewsPagination>(`${environment.APIURL}/api/Reviews?PrdId=${prdId}`)
  // }

  // AddToWishlist(prdId: number): Observable<IReview> {
  //   return this.httpClient.get<IReview>(`${environment.APIURL}/api/UserReview/${prdId}`)
  // }

  // RemoveFromWishlist(prdId: number): Observable<IReview> {
  //   return this.httpClient.get<IReview>(`${environment.APIURL}/api/UserReview/${prdId}`)
  // }



}
