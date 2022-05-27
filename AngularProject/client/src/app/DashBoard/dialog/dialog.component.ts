import { Component, ElementRef, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {MatDialog, MAT_DIALOG_DATA,MatDialogRef} from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ICategory } from 'src/app/Models/icategories';
import { IProduct } from 'src/app/Models/iproduct';
import { Category } from 'src/app/Models/Owner/IOwnerProduct';
import { IProductToAdd } from 'src/app/Models/Owner/IProductToAdd';
import { ProductToAdd } from 'src/app/Models/Owner/ProductToAdd';
import { CategoryService } from 'src/app/Services/category.service';
import { ProductsService } from 'src/app/Services/products.service';
import { ProductListComponent } from '../product-list/product-list.component';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css']
})
export class DialogComponent implements OnInit, OnDestroy{
  public ownerForm!: FormGroup;
  
  categories? : ICategory[];
  colorArray: string[];
  prevState!: ProductToAdd;
  colorIndex: number;

  constructor(@Inject(MAT_DIALOG_DATA) public data: IProduct,
  private catService: CategoryService, private productsService: ProductsService
  ,private router: Router) {
    this.colorArray = ["Blue", "Green", "Yellow", "Brown", "Pink", "Red"];
    this.prevState = new ProductToAdd();

    this.prevState = this.mappingFunction(data);
    // console.log("data = " + data.images);
    // this.prevState.color = 0;
    console.log(" this.prevState = " +  this.prevState.color);
    console.log(data.color);
    this.colorIndex = this.colorArray.findIndex(c => c == data.color);
  }
  ngOnDestroy(): void {
    this.onClose();
  }

  ngOnInit(): void {

    this.ownerForm = new FormGroup({
      title_en: new FormControl('', [Validators.required]),
      title_ar: new FormControl('', [Validators.required]),
      titleEn_details: new FormControl('', [Validators.required]),
      titleAr_details: new FormControl('', [Validators.required]),
      price: new FormControl('', [Validators.required]),
      quantity: new FormControl('', [Validators.required]),
      // color2: new FormControl('', [Validators.required])
    
    });

    this.catService.getAllCategories().subscribe((response)=>{
      this.categories = response;
    })
  }

  public hasError = (controlName: string, errorName: string) =>{
    return this.ownerForm.controls[controlName].hasError(errorName);
  }


  

  changeCat(item:any){
    console.log("Item: ");
    console.log(item.value);

    this.data.category.name = this.categories?.find(c => c.id== item.value)?.name ?? "";
    this.data.category.id = item.value;
    this.data.categoryID = item.value;
  }
  changeColor(item:any){
    console.log(item);
    this.data.color = this.colorArray[item.value];
  }

 
  private mappingFunction(product: IProduct): ProductToAdd{
    var productToAdd = new ProductToAdd();
   
    /// Mapping !
    productToAdd.id = product?.id;
    console.log(product?.category);
    
    productToAdd.categoryID = this.data.categoryID;
    console.log(productToAdd.categoryID );

    productToAdd.color = this.colorArray.findIndex(o => o == product?.color ?? "Blue");
    productToAdd.details_AR = product?.details_AR ?? "";
    productToAdd.details_EN = product?.details_EN ?? "";
    productToAdd.ownerId = "1";
    productToAdd.price = product?.price ?? 0;
    productToAdd.quantity = product?.quantity ?? 0;
    productToAdd.title_AR = product?.title_AR ?? "";
    productToAdd.title_EN = product?.title_EN ?? "";

    console.log(product);
    console.log(productToAdd);
    return productToAdd
  }
  onUpdate(){
    if(this.ownerForm.valid){
      var p = this.mappingFunction(this.data);
      this.prevState = p;
      this.productsService.updateProduct(this.data.id??0, p).subscribe((response)=>{
        console.log("Updated Successfully")
  
      },(error)=>{
        console.log(error)
      })
    }
  }

  onClose(){
    this.data.categoryID = this.prevState.categoryID;
    this.data.category.name = this.categories?.find(c => c.id== this.data.categoryID)?.name ?? "";
    this.data.category.id = this.data.categoryID;

    this.data.color = this.colorArray[this.prevState.color];

    this.data.details_AR = this.prevState.details_AR;
    this.data.details_EN = this.prevState.details_EN;
    this.data.price = this.prevState.price;
    this.data.quantity = this.prevState.quantity;
    this.data.title_AR = this.prevState.title_AR;
    this.data.title_EN = this.prevState.title_EN;

  }
}
