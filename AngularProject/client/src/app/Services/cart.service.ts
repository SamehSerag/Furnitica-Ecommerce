import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, tap } from 'rxjs/operators';
import { ShoppingCart } from '../Models/shoppingcart';

@Injectable({
  providedIn: 'root'
})





export class CartService {
  
  cartItemCount = 0;
  baseURL: string;

  constructor(private http: HttpClient) {
    this.baseURL = '/api/shoppingcart/';
  }

  AddProductToCart(productId: number) {
    // Create object of class header (Helps in requests (User token))
    let headers : HttpHeaders = new HttpHeaders().set("Authorization", "Bearer " + localStorage["access-token"]);
    
    let res = this.http.post<any>(this.baseURL, {headers, observe: "response"} ).pipe (
      tap(data => console.log(JSON.stringify(data)))
    );
    return this.http.post<number>(this.baseURL + `addToCart/${productId}`, {});
  }

  

  GetCart(userId: string) {
    return this.http.get<any>(this.baseURL + userId)
      .pipe(map((response: ShoppingCart[]) => {
        this.cartItemCount = response.length;
        return response;
      }));
  }

  removeCartItems(userId: string, productId: number) {
    return this.http.delete<number>(this.baseURL + `${userId}/${productId}`, {});
  }

  deleteOneCartItem(userId: string, productId: number) {
    return this.http.put<number>(this.baseURL + `${userId}/${productId}`, {});
  }

  clearCart(userId: string) {
    return this.http.delete<number>(this.baseURL + `${userId}`, {});
  }
}