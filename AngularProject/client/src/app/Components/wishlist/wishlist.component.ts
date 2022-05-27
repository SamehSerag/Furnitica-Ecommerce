import { Component, OnInit } from '@angular/core';
import { IWishlist } from 'src/app/Models/iwishlist';
import { WishlistService } from 'src/app/Services/wishlist.service';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.css']
})
export class WishlistComponent implements OnInit {
  wishlist: IWishlist[] = [];

  constructor(private wishlistService:WishlistService) { }

  ngOnInit(): void {
    this.wishlistService.getWishList()
    .subscribe(wishlist => {
      this.wishlist = wishlist;
      console.log(this.wishlist);
    });
  }

  RemoveFromWishlist(prdId:number)
  {
    this.wishlistService.RemoveFromWishlist(prdId).subscribe();
  }

}
