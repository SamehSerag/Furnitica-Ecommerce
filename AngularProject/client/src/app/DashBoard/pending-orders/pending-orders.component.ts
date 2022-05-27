import { Component, OnInit } from '@angular/core';
import { OrdersService } from '../../Services/orders.service';
import { IOrder } from 'src/app/Models/iorder';
@Component({
  selector: 'app-pending-orders',
  templateUrl: './pending-orders.component.html',
  styleUrls: ['./pending-orders.component.css']
})
export class PendingOrdersComponent implements OnInit {

  allOrders: IOrder[] = [];
  orderProducts: any = {};
  constructor(private _OrdersService: OrdersService) {


  }

  ngOnInit(): void {
    this._OrdersService.getAllPendingOrders().subscribe((data: IOrder[]) => {
      this.allOrders = data;
      console.log(this.allOrders);
    }, err => {
      console.log(err);
    },
      () => {
        //final
      })
  }
  /***********************************
 * Delete Order
 * *********************************** */

   DeleteOrder(OrderId: number) {
    this._OrdersService.deleteOrder(OrderId).subscribe(data => {
      
      console.log(data);

    }, error => {
      console.log(error);
    })
    console.log(OrderId);
  }
  /***********************************
 * Accept Order
 * *********************************** */
  AcceptOrders(OrderId: number) {
    debugger
    this._OrdersService.AcceptOrder(OrderId).subscribe(data => {
      debugger
      console.log(data);

    }, error => {
      console.log(error);
    })
    console.log(OrderId);
  }
  
}
