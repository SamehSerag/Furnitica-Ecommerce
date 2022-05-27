import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable, ObservableInput, tap } from "rxjs";
import { environment } from "src/environments/environment";
import { LoginDTO } from "../DTOs/LoginDTO";
import { RegisterDTO } from "../DTOs/RegisterDTO";


@Injectable({
  providedIn: 'root'
})

export class AuthService {

  static errMessage : string = "";
  static statusCode : number = 200;

  constructor(private http: HttpClient) { }

  Register (user : RegisterDTO) : Observable<any> {
    console.log("username = " + user.username);

    let res = this.http.post<any>(`${environment.AuthAPIURL}/register`, user, {observe: 'response'}).pipe(
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.habdleError)
    );
    return res;
  }

  private habdleError(err : any) : ObservableInput<any> {
    console.log("handling err: ", err);
    console.log("status-code = ", err["status"]);
    console.log("message = ", err["error"]);
    AuthService.statusCode = err["status"];
    AuthService.errMessage = err["error"];
    let res = {} as ObservableInput<any>;
    return res;
  }

  Login(user : LoginDTO) : Observable<any> {
    let res = this.http.post<any>(`${environment.AuthAPIURL}/login`, user, {observe: 'response'}).pipe(
      tap(data => console.log("data = " + JSON.stringify(data))),
      catchError(this.habdleError)
    );
    return res;
  }
  isAdmin() : Observable<any> {
    let headers : HttpHeaders = new HttpHeaders().set("Authorization", "Bearer " + localStorage["access-token"]);

    console.log("55555555555555555555");
    let res = this.http.get<any>(`${environment.AuthAPIURL}/testAdmin`, {headers, observe: 'response'}).pipe(
      tap(data => {console.log("ana alaa")}),
      catchError(this.habdleError)
      
    );
    return res;
  }

  Logout(){
    localStorage.removeItem("access-token");
    localStorage.removeItem("expiration");
    localStorage.setItem("user-data", "");
    return  this.http.get<any>(`${environment.AuthAPIURL}/logout`);
  }

  
 
}
