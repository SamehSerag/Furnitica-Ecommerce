import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { IPagination } from 'src/app/Models/ipagination';
import { IProduct } from 'src/app/Models/iproduct';
import { ProductsService } from 'src/app/Services/products.service';
import { environment } from 'src/environments/environment';

import { Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductsSearchModel } from 'src/app/Models/ProductsSearchModel';
import { Options } from "@angular-slider/ngx-slider";

@Component({
  selector: 'app-main-shop',
  templateUrl: './main-shop.component.html',
  styleUrls: ['./main-shop.component.css']
})
export class MainShopComponent implements OnInit, OnChanges {
  value: number = 0;
  highValue: number = 0;
  options: Options = {
    floor: 0,
    ceil: 1000
  };


  sortArray: string[];
  colorArray: string[];
  // sortArrayValues: string[];
  products: IProduct[];
  pagination!: IPagination;
  productsSearchModel: ProductsSearchModel;

  constructor(private prdService: ProductsService,
    @Inject(DOCUMENT) private dom: Document, private router:Router, 
    private activatedRoute: ActivatedRoute) {
    this.products=[];

    this.sortArray = ["Sort by", "Name, A to Z", "Name, Z to A",
      "Price, low to high", "Price, high to low"];
    this.colorArray = ["Blue", "Green", "Yellow", "Brown", "Pink", "Red"];
    // this.sortArrayValues = ["Sort by", "Title_EN", "Title_EN", "price", "price"];


    this.productsSearchModel = new ProductsSearchModel();
  }


  public IfImgFound(i: number, imageIndex: number): boolean {
    return this.products[i].images[imageIndex] != null ? true : false;
  }

  public Img(i: number, imageIndex: number): string {
    return this.products[i].images[imageIndex];
  }
  ngOnChanges(changes: SimpleChanges): void {
    // console.log("in onChange")
    // console.log(this.selectedSort);
  }

  ngOnInit(): void {
    if (this.activatedRoute.snapshot.paramMap.get('catId') != null)
      this.productsSearchModel.category.push(Number(this.activatedRoute.snapshot.paramMap.get('catId')));

      console.log(this.products);
    // this.prdService.getAllProducts().subscribe(
    //   response => {
    //     this.pagination = response;
    //     this.products = response.data;
    //   });
    //this.filterByCat(Number(this.activatedRoute.snapshot.paramMap.get('catId')));

    this.getProductFilteration();

    this.activatedRoute.queryParams
      .subscribe(params => {
        console.log(params['search']);
        this.productsSearchModel.search= params['search'] == undefined ? "" : params['search'];
        this.getProductFilteration();

      }
    );

    // this.router.navigate(["/main-shop"],{queryParams:this.productsSearchModel} );

    // this.prdService.getAllProducts().subscribe(
    //   response => {
    //     this.pagination = response;
    //     this.products = response.data;
    //   });
      // console.log(this.productsSearchModel.toString());

  }

  filterByCat(catId: number): void {
    this.prdService.getProductsByCatId(catId).subscribe(
      response => {
        this.pagination = response;
        this.products = response.data;
      }
    );
  }

  ChangeCategories(categories: number[]) {
    this.productsSearchModel.category = categories;
    this.getProductFilteration();
  }

  getProductFilteration() {
    this.prdService.getProductsFilteration(this.productsSearchModel.toString()).subscribe(
      response => {
        this.pagination = response;
        this.products = response.data;


        this.options.ceil = this.pagination.priceRangeObj.maxPrice; 
        this.options.floor = this.pagination.priceRangeObj.minPrice; 

        // console.log(this.pagination.priceRangeObj.minPrice);
        // console.log(this.pagination.priceRangeObj.maxPrice);
        console.log(this.productsSearchModel.maxPrice);
        console.log(this.productsSearchModel.minPrice);

        this.highValue = 
                  this.productsSearchModel.maxPrice <= 0 ?  this.options.ceil: this.productsSearchModel.maxPrice
        
        this.value = 
                  this.productsSearchModel.minPrice == 0 ?  this.options.floor: this.productsSearchModel.minPrice
                  
      })
      // this.router.navigate(["/main-shop"]);
      // this.prdService.getPriceRange()    

      console.log(this.productsSearchModel.toString())

  }


  sort(event: any): void {
    var selected = event.target.value;

    if (selected == 1 || selected == 2)
      this.productsSearchModel.sortby = "Title_EN";
    else if (selected == 3 || selected == 4)
      this.productsSearchModel.sortby = "price";
    else
      this.productsSearchModel.sortby = "";

    if (selected == 1 || selected == 3)
      this.productsSearchModel.sortdir = "asc";
    else
      this.productsSearchModel.sortdir = "dasc";

    this.getProductFilteration();
    console.log(this.products);
  }

  /// Pagination
  paginate(pageIndex: number): void {

    this.productsSearchModel.pageIndex = pageIndex;
    this.getProductFilteration();

    // this.dom.body.scrollTop =0;
    this.dom.documentElement.scrollTop = 100;


  }
  nextPage(): void {
    if (this.pagination.hasNextPage) {
      this.paginate(++this.productsSearchModel.pageIndex);
    }
  }
  previousPage(): void {
    if (this.pagination.hasPreviousPage) {
      this.paginate(--this.productsSearchModel.pageIndex);
    }
  }
  pricefun(event: any): void {
    console.log(event.target);
  }

  colorChanged(colorId: number) {
    this.productsSearchModel.color = colorId;
    this.getProductFilteration();
  }
  priceChange(){

    this.productsSearchModel.maxPrice = this.highValue;
    this.productsSearchModel.minPrice = this.value;

    this.getProductFilteration();
  }

}
