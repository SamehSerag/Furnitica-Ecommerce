import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { IOrder } from 'src/app/Models/iorder';
import { CheckOutService } from 'src/app/Services/checkOut.service';

@Component({
  selector: 'app-check-out',
  templateUrl: './check-out.component.html',
  styleUrls: ['./check-out.component.css']
})
export class CheckOutComponent implements OnInit {

  userId!: string;
  totalPrice!: number;
  IsEnglish: boolean = true;
  //checkOutItems = new IOrder();
  order: IOrder = <IOrder>{};

  private unsubscribe$ = new Subject<void>();

  constructor(private _checkOutService: CheckOutService) { }

  ngOnInit(): void {
    this._checkOutService.placeOrder().subscribe(
      response => {console.log(response)
      this.order = response.body;
      console.log(this.order);
      }
    );
  }

}
