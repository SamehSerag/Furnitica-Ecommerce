import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, retry, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IWishlist } from '../Models/iwishlist';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {

  httpOption;
  constructor(private httpClient: HttpClient) {
      this.httpOption = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
          //  , Authorization: 'my-auth-token'
        })

      };
  }

  getWishList(): Observable<IWishlist[]> {
    let headers : HttpHeaders = new HttpHeaders().set("Authorization", "Bearer " + localStorage["access-token"]);
    return this.httpClient.get<IWishlist[]>(`${environment.APIURL}/api/WishList`,{headers});
  }

  AddToWishlist(prdId: number):Observable<IWishlist[]> {
    let headers : HttpHeaders = new HttpHeaders().set("Authorization", "Bearer " + localStorage["access-token"]);
    return this.httpClient.post<IWishlist[]>(`${environment.APIURL}/api/WishList/${prdId}`, "",{headers})
    .pipe(
      retry(2),
      catchError(this.handleError)
      )
    }

    RemoveFromWishlist(prdId: number){
    let headers : HttpHeaders = new HttpHeaders().set("Authorization", "Bearer " + localStorage["access-token"]);
    return this.httpClient.delete(`${environment.APIURL}/api/WishList/${prdId}`,{headers})
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
