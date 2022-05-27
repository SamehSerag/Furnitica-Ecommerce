import { Component, OnInit } from '@angular/core';
import { OrdersService } from '../../Services/orders.service';
import { IOrder } from 'src/app/Models/iorder';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ConditionalExpr } from '@angular/compiler';

@Component({
  selector: 'app-all-orders',
  templateUrl: './all-orders.component.html',
  styleUrls: ['./all-orders.component.css']
})
export class AllOrdersComponent implements OnInit {

  allOrders: IOrder[] = [];
  FilterdData: any = [];
  orderProducts: any = {};
  IsFilterd:boolean=false;
  isShown: boolean = false; // hidden by default
  FilterOn: any = ['Pending', 'Accepted', 'Rejected']
  form = new FormGroup({
    website: new FormControl('', Validators.required)
  });
  get f() {
    return this.form.controls;
  }
  constructor(private _OrdersService: OrdersService) {
  }

  ngOnInit(): void {
    this._OrdersService.getAllAdminOrders().subscribe((data: IOrder[]) => {
      this.allOrders = data;
      console.log("admin",this.allOrders);
    }, err => {
      console.log(err);
    },
      () => {
        //final
      })
  }
 
  /*****************************
   * 
   * FILTER ORDERS
   * 
   ***************************** */
  FilterAllOrders(e: any) {
    let dataFilter=e.target.value;
    this._OrdersService.getOrderssFilteration(dataFilter).subscribe(data => {
      this.FilterdData=data;
      this.IsFilterd=true;
      console.log("TypeOf",typeof(data))
      if(data==null){
        console.log("No Data");
      }
      console.log("Nono",dataFilter);
      console.log("Filter", data);
    }, err => {
      console.log(err);
    },
      () => {
        //final
      })
    console.log(e.target.value);
  }
  ShowAllOrders(){
    this.IsFilterd=false;
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
