import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { IProduct } from 'src/app/Models/iproduct';
import { ProductsService } from 'src/app/Services/products.service';

@Component({
  selector: 'app-main-shop',
  templateUrl: './main-shop.component.html',
  styleUrls: ['./main-shop.component.css']
})
export class MainShopComponent implements OnInit, OnChanges {
  products: IProduct[] = [];
  constructor(private prdService: ProductsService) { }
  

  public IfImgFound(i:number, imageIndex: number): boolean{
    return this.products[i].images[imageIndex] != null ? true : false;
  }

  public Img(i:number, imageIndex: number): string{
    return this.products[i].images[imageIndex];
  }
  ngOnChanges(changes: SimpleChanges): void {
    this.prdService.getProductsByCatId(1).subscribe(
      response => {
        this.products = response.data;
      });
  }

  ngOnInit(): void {

    this.prdService.getAllProducts().subscribe(
      response => {
        this.products = response.data;
      });
    // this.http.get('https://localhost:7231/api/products').subscribe(
    //   (response: any) => {
    //     this.products =  response.data;
    //     console.log("Test");
    //     console.log(this.products);
    //   },
    //   (error) => console.log(error)
    // )
  }
  filterByCat(catId:number):void{
    this.prdService.getProductsByCatId(catId).subscribe(
      response => {
        this.products = response.data;
      }
    );
  }

}
