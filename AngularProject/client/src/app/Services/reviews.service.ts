import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, retry, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IReview, IReviewsPagination } from '../Models/ireviews';

@Injectable({
  providedIn: 'root'
})

export class ReviewsService {
  httpOption;
  constructor(private httpClient: HttpClient) {
    this.httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
        , Authorization: 'my-auth-token'
      })
    };
  }

  getAllReviews(prdId: number): Observable<IReviewsPagination> {
    return this.httpClient.get<IReviewsPagination>(`${environment.APIURL}/api/Reviews?PrdId=${prdId}`)
  }

  getUserReview(prdId: number): Observable<IReview> {
    return this.httpClient.get<IReview>(`${environment.APIURL}/api/UserReview/${prdId}`)
  }

  getReviewById(id: number): Observable<IReview> {
    return this.httpClient.get<IReview>(`${environment.APIURL}/api/Reviews/${id}`)
  }

  postReview(review: IReview): Observable<IReview> {
    return this.httpClient.post<IReview>(`${environment.APIURL}/api/Reviews`,
      JSON.stringify(review), this.httpOption)
      .pipe(
        retry(2),
        catchError(this.handleError)
      )
  }


  putReview(review: IReview) {
    return this.httpClient.put<IReview>(`${environment.APIURL}/api/Reviews/${review.id}`,
    JSON.stringify(review), this.httpOption)
    .pipe(
      retry(2),
      catchError(this.handleError)
    )
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }
}
