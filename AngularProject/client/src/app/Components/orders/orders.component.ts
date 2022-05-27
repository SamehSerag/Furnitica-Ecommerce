import { Component, ComponentFactoryResolver, OnInit } from '@angular/core';
import { OrdersService } from '../../Services/orders.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { IOrder } from 'src/app/Models/iorder';
@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  allOrders: IOrder[] = [];
  FilterdData: any = [];
  orderProducts: any = {};
  IsFilterd: boolean = false;
  isShown: boolean = false; // hidden by default
  IsEnglish: boolean = true;
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
    this._OrdersService.getAllOrders().subscribe((data: IOrder[]) => {
      this.allOrders = data;
      console.log(this.allOrders);
      if (localStorage.getItem('Lang') == 'ar') {
        this.IsEnglish = false;
        console.log("ara",localStorage.getItem('Lang'));
      }
      else {
        this.IsEnglish = true;
      }
    }, err => {
      console.log(err);
    },
      () => {
        //final
      })
  }
  FilterAllOrders(e: any) {
    let dataFilter = e.target.value;
    this._OrdersService.getOrderssFilteration(dataFilter).subscribe(data => {
      this.FilterdData = data;
      this.IsFilterd = true;

    }, err => {
      console.log(err);
    },
      () => {
        //final
      })
    console.log(e.target.value);
  }
  ShowAllOrders() {
    this.IsFilterd = false;
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