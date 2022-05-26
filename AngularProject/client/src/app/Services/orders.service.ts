import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IOrder } from '../Models/iorder';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(private httpClient: HttpClient) { }

  getAllOrders() {
    return this.httpClient.get<IOrder[]>(`${environment.APIURL}/api/Orders`)
  }

  deleteOrder(orderID: number) {
    let headers: HttpHeaders = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    headers = headers.append('Authorization', 'Bearer ' + localStorage.getItem("token"));
    
    return this.httpClient.post<number>(`${environment.APIURL}/api/Orders/rejectOrder/` + orderID, { headers: headers })
  }
  AcceptOrder(orderID: number) {
    let headers: HttpHeaders = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    headers = headers.append('Authorization', 'Bearer ' + localStorage.getItem("token"));
    
    return this.httpClient.post<number>(`${environment.APIURL}/api/Orders/acceptOrder/` + orderID, { headers: headers })
  }
  /////////////////Admin///////////////////////////
  getAllPendingOrders() {
    return this.httpClient.get<IOrder[]>(`${environment.APIURL}/api/Orders/PendingOrders`)
  }
}
