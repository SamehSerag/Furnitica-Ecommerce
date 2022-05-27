import { Component, OnInit, OnDestroy } from '@angular/core';
import { ShoppingCart } from 'src/app/Models/shoppingcart';
import { CartService } from 'src/app/Services/cart.service';
import { Subject } from 'rxjs';
// import { takeUntil } from 'rxjs/operators';
import { SubscriptionService } from 'src/app/Services/subscription.service';

@Component({
  selector: 'app-shoppingcart',
  templateUrl: './shoppingcart.component.html',
  styleUrls: ['./shoppingcart.component.css']
})
export class ShoppingcartComponent implements OnInit, OnDestroy {
  public cartItems!: ShoppingCart[];
  userId!: string;
  totalPrice!: number;
  private unsubscribe$ = new Subject<void>();
  isLoading!: boolean;

  constructor(private cartService: CartService,
    private subscriptionService: SubscriptionService) {
    this.userId = localStorage.getItem('userId') as string;
  }

  ngOnInit() {
    this.cartItems = [];
    this.isLoading = true;
    this.getShoppingCartItems();
  }

  update(res : any) {
    this.isLoading = false;
      this.cartItems = res["body"];
      console.log("cart items : ", this.cartItems);
      console.log("cart items count : ", this.cartItems.length);
      console.log("cart ts response: ", res);
      this.getTotalPrice();
      CartService.cartItemCount = this.cartItems.length;
  }


  getTotalPrice() {
    this.totalPrice = 0;
    this.cartItems.forEach(item => {
      this.totalPrice += (item.product.price * item.quantity);
    });
  }

  getShoppingCartItems() {
    this.isLoading = true;

    this.cartService.GetCart().subscribe({
      next: (res) => {
        //todo: updata cart item count
        this.update(res);
      },
      error: (err) => {
        // this.isLoading = false;
        console.log("cart ts ", err);
      },
      complete: () => {
        console.log("cart ts request completed!")
      }
    });
    this.isLoading = false;
  }

  deleteCartItem(productId: number) {
    this.isLoading = true;
    this.cartService.removeCartItems(productId).subscribe({
      next: (res) => {
        //todo: updata cart item count
        this.update(res);
      },
      error: (err) => {
        console.log("cart ts ", err);
      },
      complete: () => {
        console.log("cart ts request completed!")
      }
    });

    this.isLoading = false;
  }

  addToCart(productId: number) {
    this.isLoading = true;
    this.cartService.AddProductToCart(productId).subscribe({
      next: (res) => {
        //todo: updata cart item count
        console.log("cart ts response: ", res);
        this.update(res);
      },
      error: (err) => {
        console.log("cart ts ", err);
        console.log('Error ocurred while addToCart data : ', err);
      },
      complete: () => {
        console.log("cart ts request completed!")
      }
    });
    this.isLoading = false;
  }

  deleteOneCartItem(productId: number) {
    this.isLoading = true;
    this.cartService.deleteOneCartItem(productId).subscribe({
      next: (res) => {
        //todo: updata cart item count
        this.update(res);
        console.log("cart ts response: ", res);
      },
      error: (err) => {
        console.log("cart ts ", err);
      },
      complete: () => {
        console.log("cart ts request completed!")
      }
    });

    this.isLoading = false;
  }

  clearCart() {
    this.isLoading = true;
    this.cartService.clearCart().subscribe({
      next: (res) => {
        this.update(res);
        //todo: updata cart item count
        console.log("cart ts response: ", res);
      },
      error: (err) => {
        console.log("cart ts ", err);
        console.log('Error ocurred while deleting cart item : ', err);
      },
      complete: () => {
        console.log("cart ts request completed!")
      }
    });

    this.isLoading = false;
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }


}
