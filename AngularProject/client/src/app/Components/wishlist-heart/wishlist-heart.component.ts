import { Component, Input, OnInit } from '@angular/core';
import { IWishlist } from 'src/app/Models/iwishlist';
import { WishlistService } from 'src/app/Services/wishlist.service';

@Component({
  selector: 'app-wishlist-heart',
  templateUrl: './wishlist-heart.component.html',
  styleUrls: ['./wishlist-heart.component.css']
})
export class WishlistHeartComponent implements OnInit {
  @Input() productId?: number = 0;
  wishlist: IWishlist[] = [];

  constructor(private wishlistService: WishlistService) { }

  ngOnInit(): void {
    this.wishlistService.getWishList().subscribe(wishlist => {
      this.wishlist = wishlist;
    });
  }

  InWishlist(): boolean {
    return this.wishlist.find(w => w.productId == this.productId) == null ? false : true;
  }

  RemoveFromWishList() {
    if (this.productId) {
      this.wishlistService.RemoveFromWishlist(this.productId).subscribe(response => {
        this.ngOnInit();
      });

    }
  }

  AddToWishList() {
    if (this.productId)
      this.wishlistService.AddToWishlist(this.productId).subscribe(response => {
        this.ngOnInit();
      });
  }
}
