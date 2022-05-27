import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IOrder } from '../Models/iorder';
import { environment } from 'src/environments/environment';
import { catchError, Observable, ObservableInput, tap } from 'rxjs';
import { CartService } from './cart.service';

@Injectable({
  providedIn: 'root'
})
export class CheckOutService {

    baseURL: string;
    
  constructor(private http: HttpClient) {
    this.baseURL = environment.BaseUrl;
  }

  private habdleError(err : any) : ObservableInput<any> {
    console.log("checkout handling err: ", err);
    console.log("checkout status-code = ", err["status"]);
    console.log("checkout message = ", err["error"]);
    CartService.statusCode = err["status"];
    CartService.errMessage = err["error"];
    let res = {} as ObservableInput<any>;
    return res;
  }


  placeOrder() : Observable<any>  {
    let url = this.baseURL + `/CheckOut`;
    let headers : HttpHeaders = new HttpHeaders().set("Authorization", "Bearer " + localStorage["access-token"]);
    let res = this.http.post<any>(url, "", {headers, observe: "response"} ).pipe (
        tap(data => console.log(JSON.stringify(data))),
        catchError(this.habdleError)
      );
    return res;
  }
}

