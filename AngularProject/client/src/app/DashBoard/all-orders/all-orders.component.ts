import { Component, OnInit } from '@angular/core';
import { OrdersService } from '../../Services/orders.service';
import { IOrder } from 'src/app/Models/iorder';
@Component({
  selector: 'app-all-orders',
  templateUrl: './all-orders.component.html',
  styleUrls: ['./all-orders.component.css']
})
export class AllOrdersComponent implements OnInit {

  allOrders: IOrder[] = [];
  orderProducts: any = {};
  constructor(private _OrdersService: OrdersService) {


  }

  ngOnInit(): void {
    this._OrdersService.getAllOrders().subscribe((data: IOrder[]) => {
      this.allOrders = data;
      console.log(this.allOrders);
    }, err => {
      console.log(err);
    },
      () => {
        //final
      })
  }
  DeleteOrder(OrderId: number)
  {
    this._OrdersService.deleteOrder(OrderId).subscribe(data=>{
      console.log(data);
      
    },error=>{
      console.log(error);
    })
    console.log(OrderId);
  }
  AcceptOrders(OrderId: number)
  {
    this._OrdersService.AcceptOrder(OrderId).subscribe(data=>{
      console.log(data);
      
    },error=>{
      console.log(error);
    })
    console.log(OrderId);
  }
}
