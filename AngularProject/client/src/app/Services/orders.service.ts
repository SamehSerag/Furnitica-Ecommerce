import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IOrder } from '../Models/iorder';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  auth_token = localStorage.getItem("access-token");

  constructor(private httpClient: HttpClient) { }

  getAllOrders() {
    let headers: HttpHeaders = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    headers = headers.append('Authorization', 'Bearer ' + localStorage.getItem("access-token"));
    return this.httpClient.get<IOrder[]>(`${environment.APIURL}/api/Orders`,{headers: headers})
  }
  getOrderssFilteration(filteration:string) {
    let headers: HttpHeaders = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    headers = headers.append('Authorization', 'Bearer ' + localStorage.getItem("access-token"));
    return this.httpClient.get<IOrder>(`${environment.APIURL}/api/Orders?OrdeState=${filteration}`,{headers: headers});
  }
  deleteOrder(orderID: number) {
    let headers: HttpHeaders = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    headers = headers.append('Authorization', 'Bearer ' + localStorage.getItem("token"));

    return this.httpClient.put<number>(`${environment.APIURL}/api/Orders/rejectOrder/` + orderID, { headers: headers })
  }
  AcceptOrder(orderID: number) {
    let headers: HttpHeaders = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    headers = headers.append('Authorization', 'Bearer ' + localStorage.getItem("access-token"));

    console.log("Token",this.auth_token)
    return this.httpClient.put<number>(`${environment.APIURL}/api/Orders/acceptOrder/` + orderID, { headers: headers })
  }
  /////////////////Admin///////////////////////////
  getAllPendingOrders() {
    return this.httpClient.get<IOrder[]>(`${environment.APIURL}/api/Orders/PendingOrders`)
  }
  ///////////////////ADMIN API/////////////////////
  getAllAdminOrders() {
    let headers: HttpHeaders = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    headers = headers.append('Authorization', 'Bearer ' + localStorage.getItem("access-token"));
    return this.httpClient.get<IOrder[]>(`${environment.APIURL}/api/Orders/AdminOrders`,{headers: headers})
  }
}
