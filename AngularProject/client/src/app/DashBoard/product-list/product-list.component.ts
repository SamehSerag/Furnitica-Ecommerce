import { DOCUMENT } from '@angular/common';
import { AfterViewChecked, Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { IPagination } from 'src/app/Models/ipagination';
import { IProduct } from 'src/app/Models/iproduct';
import { Category, IOwnerProduct } from 'src/app/Models/Owner/IOwnerProduct';
import { IProductToAdd } from 'src/app/Models/Owner/IProductToAdd';
import { ProductToAdd } from 'src/app/Models/Owner/ProductToAdd';
import { ProductsSearchModel } from 'src/app/Models/ProductsSearchModel';
import { CategoryService } from 'src/app/Services/category.service';
import { ProductsService } from 'src/app/Services/products.service';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit, AfterViewChecked{
  @ViewChild('scrollBottom') scrollBottom!: ElementRef;

  products: IProduct[];
  productToDelete?: IProduct ;
  pagination!: IPagination;
  productsSearchModel: ProductsSearchModel;
  colorArray: string[];

  ownerId: string="1";

  constructor(@Inject(DOCUMENT) private dom: Document,
    private prdService: ProductsService, private router: Router, public dialog: MatDialog
    , private catServ: CategoryService) {
      this.colorArray = ["Blue", "Green", "Yellow", "Brown", "Pink", "Red"];
    this.products = [];
    this.productsSearchModel = new ProductsSearchModel();
    this.productToDelete = undefined;
    // this.pagination  = { hasPreviousPage: false, totalPages: 0, hasNextPage: false};
  }
  ngAfterViewChecked(): void {
    // console.log(this.scrollBottom.nativeElement.scrollHeight)
    
  }

  ngOnInit(): void {
    this.productsSearchModel.ownerId = this.ownerId;
    this.getProductFilteration();
  }
  openDialog2() {
    this.dialog.open(DialogComponent, {
      // data: {
      //   animal: 'panda',
      // },
      data: {
        products: this.products[0],
      },
    });
  }
  openDialog(prId:number) {
    // var product = this.products.find(p => p.id == prId); 
    // var productToAdd = new ProductToAdd();
   
    // /// Mapping !
    // productToAdd.id = product?.id;
    // this.catServ.getAllCategories().subscribe((respons)=>
    // {
    //   productToAdd.categoryID = respons.find(c => c.name == product?.category)?.id ?? 1
    // });
    // productToAdd.color = this.colorArray.findIndex(o => o == product?.color ?? "Blue");
    // productToAdd.details_AR = product?.details_AR ?? "";
    // productToAdd.details_EN = product?.details_EN ?? "";
    // productToAdd.ownerId = this.ownerId;
    // productToAdd.price = product?.price ?? 0;
    // productToAdd.quantity = product?.quantity ?? 0;
    // productToAdd.title_AR = product?.title_AR ?? "";
    // productToAdd.title_EN = product?.title_EN ?? "";

    // console.log(product);
    // console.log(productToAdd);

    this.dialog.open(DialogComponent, {
      data: this.products.find(p => p.id == prId) ,
    });
    
    // console.log("test");

  }
  func(catId:number){
    var ss ;
    this.catServ?.getAllCategories().subscribe((s)=>{
     ss =  s.find(c=> c.id==catId)?.name ?? 0
    })
    return ss;
  }


  getProductFilteration() {
    this.prdService.getProductsFilteration(this.productsSearchModel.toString()).subscribe(
      response => {
        this.pagination = response;
        this.products = response.data;
      });
  }
  scrollToBottom(): void {
    try {
      this.dom.documentElement.scrollTop = this.scrollBottom.nativeElement.scrollHeight;
      
    } catch (err) { }
  }

  scrollToTop(): void {
    try {
      this.dom.documentElement.scrollTop = 100;
    } catch (err) { }
  }
  

  onSelect(product: IProduct) {

  }
  onUpdate(product: IProduct) {

  }
  onDelete(product: IProduct) {
    this.productToDelete = product;   
  }
  Delete(){
    if(this.productToDelete != null){
      const observer = {
        next: () => {
          // console.log(this.products.length)
          // alert("Deleted Successfully");
          this.ngOnInit();
          // this.scrollToTop();
          this.productToDelete = undefined;
        },
        Error: () => {
          alert("Error !!");
        }
  
      }
      this.prdService.deleteProduct(this.productToDelete.id).subscribe(observer);
    }
    
  }

 

  /// Pagination
  paginate(pageIndex: number): void {

    this.productsSearchModel.pageIndex = pageIndex;
    this.getProductFilteration();

    // this.dom.body.scrollTop =0;
    this.scrollToTop();
    

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
 
}


 // AddProduct() {
  //   const observer = {
  //     next: (prd: IOwnerProduct) => {
  //       alert("Add Successfully");
  //       this.productsSearchModel.pageIndex = this.pagination.totalPages;
  //       this.ngOnInit();
  //       this.scrollToBottom();
  //     },
  //     Error: (prd: IOwnerProduct) => {
  //       alert("Error !!");
  //     }

  //   }

  //   const product: IProductToAdd = {
  //     title_EN: "test",
  //     title_AR: "تست",
  //     details_EN: "string",
  //     details_AR: "string",
  //     price: 10,
  //     color: 1,
  //     quantity: 1,
  //     categoryID: 1,
  //     ownerId: "1",
  //   }
  //   this.prdService.addProduct(product).subscribe(observer);
  // }
