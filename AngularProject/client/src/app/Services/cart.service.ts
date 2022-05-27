import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ObservableInput, catchError, map, tap, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ShoppingCart } from '../Models/shoppingcart';

@Injectable({
  providedIn: 'root'
})

export class CartService {

  static cartItemCount = 0;
  baseURL: string;
  static errMessage : string = "";
  static statusCode : number = 200;

  constructor(private http: HttpClient) {
    // this.baseURL = '/api/shoppingcart/';
    this.baseURL = environment.BaseUrl;
  }


  private habdleError(err : any) : ObservableInput<any> {
    console.log("cart handling err: ", err);
    console.log("cart status-code = ", err["status"]);
    console.log("cart message = ", err["error"]);
    CartService.statusCode = err["status"];
    CartService.errMessage = err["error"];
    let res = {} as ObservableInput<any>;
    return res;
  }


  AddProductToCart(productId: number) : Observable<any> {

    let url = this.baseURL + `/Cart/AddToCart/${productId}`;
    let headers : HttpHeaders = new HttpHeaders().set("Authorization", "Bearer " + localStorage["access-token"]);

    console.log("cart req has token:", headers.has('Authorization'));
    console.log("cart url: ", url);
    console.log("cart access token", localStorage["access-token"]);
    console.log("cart req header", headers);

    let res = this.http.post<any>(url, "", {headers, observe: "response"} ).pipe (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.habdleError)
    );

    return res;
  }

  GetCart() : Observable<any> {

    let url = this.baseURL + `/Cart/GetAllProducts`;
    let headers : HttpHeaders = new HttpHeaders().set("Authorization", "Bearer " + localStorage["access-token"]);


    let res = this.http.get<any>(url, {headers, observe: "response"} ).pipe (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.habdleError)
    );
    return res;
  }

  //* delete all instances of one item
  removeCartItems(productId: number) : Observable<any> {
    let url = this.baseURL + `/Cart/${productId}`;
    let headers : HttpHeaders = new HttpHeaders().set("Authorization", "Bearer " + localStorage["access-token"]);


    let res = this.http.delete<any>(url, {headers, observe: "response"} ).pipe (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.habdleError)
    );
    return res;

    // return this.http.delete<number>(this.baseURL + `${userId}/${productId}`, {});
  }

  //* delete one instance of an item (decrease count by one)
  deleteOneCartItem(productId: number) {
    let url = this.baseURL + `/Cart/${productId}`;
    let headers : HttpHeaders = new HttpHeaders().set("Authorization", "Bearer " + localStorage["access-token"]);


    let res = this.http.put<any>(url, "", {headers, observe: "response"} ).pipe (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.habdleError)
    );

    return res;
    // return this.http.put<number>(this.baseURL + `${userId}/${productId}`, {});
  }

  clearCart() {
    let url = this.baseURL + `/Cart`;
    let headers : HttpHeaders = new HttpHeaders().set("Authorization", "Bearer " + localStorage["access-token"]);


    let res = this.http.delete<any>(url, {headers, observe: "response"} ).pipe (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.habdleError)
    );
    return res;

    // return this.http.delete<number>(this.baseURL + `${userId}`, {});
  }



}
