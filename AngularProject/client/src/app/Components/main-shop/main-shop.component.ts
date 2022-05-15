import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { IPagination } from 'src/app/Models/ipagination';
import { IProduct } from 'src/app/Models/iproduct';
import { ProductsService } from 'src/app/Services/products.service';
import { environment } from 'src/environments/environment';

import { Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-main-shop',
  templateUrl: './main-shop.component.html',
  styleUrls: ['./main-shop.component.css']
})
export class MainShopComponent implements OnInit, OnChanges {
  products: IProduct[];
  pagination!: IPagination;
  filterationQuary:string;
  @Input() pageIndex:number;
  constructor(private prdService: ProductsService, @Inject(DOCUMENT) private dom: Document) {
    this.products=[];
    this.filterationQuary = `?`;
    this.pageIndex = 1;
   }
  

  public IfImgFound(i:number, imageIndex: number): boolean{
    return this.products[i].images[imageIndex] != null ? true : false;
  }

  public Img(i:number, imageIndex: number): string{
    return this.products[i].images[imageIndex];
  }
  ngOnChanges(changes: SimpleChanges): void {
    // this.prdService.getProductsByCatId(1).subscribe(
    //   response => {
    //     this.products = response.data;
    //   });
    
  }

  ngOnInit(): void {

    this.prdService.getAllProducts().subscribe(
      response => {
        this.pagination = response;
        this.products = response.data;
      });
  }
  filterByCat(catId:number):void{
    this.prdService.getProductsByCatId(catId).subscribe(
      response => {
        this.pagination = response;
        this.products = response.data;
      }
    );
  }

  /// Pagination

  paginate(pageIndex:number):void{
    
      var filter:string = this.filterationQuary + `PageIndex=${pageIndex}`;
      this.pageIndex = pageIndex;
      
      this.prdService.getProductsFilteration(filter).subscribe(
        response => {
          this.pagination = response;
          this.products = response.data;
        })

        // this.dom.body.scrollTop =0;
        this.dom.documentElement.scrollTop=100;


  }
  nextPage(): void{
    if(this.pagination.hasNextPage){
      this.pageIndex++;
      this.paginate(this.pageIndex);
    }
  }
  previousPage():void{
    if(this.pagination.hasPreviousPage){
      this.pageIndex--;
      this.paginate(this.pageIndex);
    }
  }
}
    // this.http.get('https://localhost:7231/api/products').subscribe(
    //   (response: any) => {
    //     this.products =  response.data;
    //     console.log("Test");
    //     console.log(this.products);
    //   },
    //   (error) => console.log(error)
    // )