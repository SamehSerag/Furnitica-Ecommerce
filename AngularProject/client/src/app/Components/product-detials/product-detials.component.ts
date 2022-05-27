import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IProduct } from 'src/app/Models/iproduct';
import { ProductsService } from 'src/app/Services/products.service';

@Component({
  selector: 'app-product-detials',
  templateUrl: './product-detials.component.html',
  styleUrls: ['./product-detials.component.css']
})
export class ProductDetialsComponent implements OnInit {
  product: IProduct;
  productId?: number;
  quantity: number;
  colorArray: string[];
  ReviewCount: number = 0;

  constructor(private productService: ProductsService, private activatedRoute: ActivatedRoute,
    private router: Router) {
    this.product = {} as IProduct;
    this.colorArray = ["Blue", "Green", "Yellow", "Brown", "Pink", "Red"];
    this.quantity = 1;
  }

  ngOnInit(): void {
    this.productId = Number(this.activatedRoute.snapshot.paramMap.get('pid'));

    this.productService.getProductById(this.productId).subscribe(
      (response) => {
        this.product = response;
        if (this.product.quantity == 0)
          this.quantity = 0;
      }, (error) => {
        this.router.navigate(['/error'])
      });
  }
  incrementQuantity(): void {
    if (this.quantity < this.product.quantity)
      this.quantity++;
  }
  decrementQuantity(): void {
    if (this.quantity > 1)
      this.quantity--;

  }
  onKeyChange(event: any) {
    if (event.target.value <= 0)
      this.quantity = 1;
    else if (event.target.value > this.quantity)
      this.quantity = this.product.quantity;
  }

  GetReviewCount(count: number) {
    this.ReviewCount = count;
  }


}
