import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { IPagination } from 'src/app/Models/ipagination';
import { IProduct } from 'src/app/Models/iproduct';
import { ProductsService } from 'src/app/Services/products.service';
import { environment } from 'src/environments/environment';

import { Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { Router } from '@angular/router';
import { ProductsSearchModel } from 'src/app/Models/ProductsSearchModel';

@Component({
  selector: 'app-main-shop',
  templateUrl: './main-shop.component.html',
  styleUrls: ['./main-shop.component.css']
})
export class MainShopComponent implements OnInit, OnChanges {
  sortArray: string[];
  colorArray: string[];
  // sortArrayValues: string[];
  products: IProduct[];
  pagination!: IPagination;
  productsSearchModel: ProductsSearchModel;

  

  constructor(private prdService: ProductsService,
    @Inject(DOCUMENT) private dom: Document, private router:Router) {
    this.products=[];
    this.sortArray = ["Sort by", "Name, A to Z", "Name, Z to A",
                      "Price, low to high", "Price, high to low"];
    this.colorArray = ["Blue", "Green", "Yellow", "Brown", "Pink", "Red"];
    // this.sortArrayValues = ["Sort by", "Title_EN", "Title_EN", "price", "price"];


    this.productsSearchModel = new ProductsSearchModel();
   }


  public IfImgFound(i:number, imageIndex: number): boolean{
    return this.products[i].images[imageIndex] != null ? true : false;
  }

  public Img(i:number, imageIndex: number): string{
    return this.products[i].images[imageIndex];
  }
  ngOnChanges(changes: SimpleChanges): void {
    // console.log("in onChange")
    // console.log(this.selectedSort);
  }

  ngOnInit(): void {

    this.prdService.getAllProducts().subscribe(
      response => {
        this.pagination = response;
        this.products = response.data;
      });
      // console.log(this.productsSearchModel.toString());
  }

  filterByCat(catId:number):void{
    this.prdService.getProductsByCatId(catId).subscribe(
      response => {
        this.pagination = response;
        this.products = response.data;
      }
    );
  }

  getProductFilteration(){
    this.prdService.getProductsFilteration(this.productsSearchModel.toString()).subscribe(
      response => {
        this.pagination = response;
        this.products = response.data;
      })

      this.router.navigate(["/main-shop"]);

      console.log(this.productsSearchModel.toString())
  }


  sort(event:any):void {
    var selected = event.target.value;

    if(selected == 1 || selected == 2)
      this.productsSearchModel.sortby ="Title_EN";
    else if( selected == 3 || selected == 4)
      this.productsSearchModel.sortby = "price";
    else
      this.productsSearchModel.sortby="";

    if(selected == 1 || selected == 3)
      this.productsSearchModel.sortdir = "asc";
    else
      this.productsSearchModel.sortdir = "dasc";

    this.getProductFilteration();
    console.log(this.products);
  }

  /// Pagination
  paginate(pageIndex:number):void{

    this.productsSearchModel.pageIndex = pageIndex;
    this.getProductFilteration();

    // this.dom.body.scrollTop =0;
    this.dom.documentElement.scrollTop=100;


  }
  nextPage(): void{
    if(this.pagination.hasNextPage){
      this.paginate(++this.productsSearchModel.pageIndex);
    }
  }
  previousPage():void{
    if(this.pagination.hasPreviousPage){
      this.paginate(--this.productsSearchModel.pageIndex);
    }
  }
  pricefun(event:any):void{
    console.log(event.target);
  }

  colorChanged(colorId:number){
    this.productsSearchModel.color = colorId;
    this.getProductFilteration();
  }
}
