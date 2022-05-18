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
  
}
