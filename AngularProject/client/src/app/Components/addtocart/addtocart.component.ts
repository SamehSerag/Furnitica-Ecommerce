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

  constructor(private cartService: CartService, private subscriptionService: SubscriptionService) {
    this.userId = localStorage.getItem('userId') as string;
  }
  addToCart()
  {
    this.cartService.AddProductToCart(this.productId).subscribe(
      result => {
        this.subscriptionService.cartItemcount$.next(result);
      });
  }
  ngOnInit(): void {
  }

}