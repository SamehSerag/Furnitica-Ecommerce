import { Component, Input, OnInit } from '@angular/core';
import { CartService } from 'src/app/Services/cart.service';
import { SubscriptionService } from 'src/app/Services/subscription.service';

@Component({
  selector: 'app-addtocart',
  templateUrl: './addtocart.component.html',
  styleUrls: ['./addtocart.component.css']
})
export class AddtocartComponent implements OnInit {

  @Input()
  productId!: number;
  userId: string;

  constructor(
    private cartService: CartService,
    private subscriptionService: SubscriptionService) {
    this.userId = localStorage.getItem('userId') as string;
  }
  addToCart()
  {
    this.cartService.AddProductToCart(this.productId).subscribe({
      next: (res) => {
        console.log("res : ", res);
        this.subscriptionService.cartItemcount$.next(res);
        //todo: increese icon by 1

      },
      error: (err) => {

      },
      complete : () => {

      }
    });
  }
  ngOnInit(): void {
  }

}
