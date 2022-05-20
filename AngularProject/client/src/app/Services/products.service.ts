import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, retry, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IPagination } from '../Models/ipagination';
import { IProduct } from '../Models/iproduct';
import { IOwnerProduct } from '../Models/Owner/IOwnerProduct';
import { IProductToAdd } from '../Models/Owner/IProductToAdd';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  httpOption;
  constructor(private httpClinet: HttpClient) {
    this.httpOption = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
        // ,Authorization: 'my-auth-token'
      })
    };
  }
  // Authorization: 'my-auth-token'

  getAllProducts(): Observable<IPagination> {
    return this.httpClinet.get<IPagination>(`${environment.APIURL}/api/products`);
  }
  getProductsFilteration(filteration:string): Observable<IPagination> {
    return this.httpClinet.get<IPagination>(`${environment.APIURL}/api/products${filteration}`);
  }

  getProductsByCatId(catId: number): Observable<IPagination>{
    return this.httpClinet.get<IPagination>(`${environment.APIURL}/api/products?Category=${catId}`);
  }
  getProductById(prdId: number): Observable<IProduct>{
    return this.httpClinet.get<IProduct>(`${environment.APIURL}/api/products/${prdId}`);
  }

  addProduct(newPrd: IProductToAdd): Observable<IOwnerProduct>{
    return this.httpClinet.post<IOwnerProduct>(`${environment.APIURL}/api/products/owner`, 
    JSON.stringify(newPrd),this.httpOption)
    .pipe(
      retry(2),
      catchError(this.handleError)
    )
  }
  updateProduct(prdId: number, updatedProduct: IProduct){

  }
  deleteProduct(prdId: number){
    console.log(prdId)
    return this.httpClinet.delete(`${environment.APIURL}/api/products/owner/${prdId}`)
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
