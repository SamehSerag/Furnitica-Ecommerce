import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable, ObservableInput, tap } from "rxjs";
import { environment } from "src/environments/environment";
import { IUser } from "../Models/IUser";


@Injectable({
  providedIn: 'root'
})

export class ProfileService {

  static errMessage : string = "";
  static statusCode : number = 200;

  constructor(private http: HttpClient) { }

  getProfile () : Observable<any> {
    let headers : HttpHeaders = new HttpHeaders().set("Authorization", "Bearer " + localStorage["access-token"]);
    // let headers : HttpHeaders = new HttpHeaders().set("Authorization", "Bearer " + localStorage["access-token"]);
    let url = `${environment.BaseUrl}/User`;
    let res = this.http.get<any>(url, {headers, observe: "response"} ).pipe (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.habdleError)
    );
    return res;
  }

  private habdleError(err : any) : ObservableInput<any> {
    console.log("handling err: ", err);
    console.log("status-code = ", err["status"]);
    console.log("message = ", err["error"]);
    ProfileService.statusCode = err["status"];
    ProfileService.errMessage = err["error"];
    let res = {} as ObservableInput<any>;
    return res;
  }

  updateProfile (user : IUser) : Observable<any> {
    let headers : HttpHeaders = new HttpHeaders().set("Authorization", "Bearer " + localStorage["access-token"]);
    // let headers : HttpHeaders = new HttpHeaders().set("Authorization", "Bearer " + localStorage["access-token"]);

    let url = `${environment.BaseUrl}/User`;
    console.log("cart req header", headers);

    let res = this.http.put<any>(url, user, {headers, observe: "response"} ).pipe (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.habdleError)
    );
    return res;
  }

}
