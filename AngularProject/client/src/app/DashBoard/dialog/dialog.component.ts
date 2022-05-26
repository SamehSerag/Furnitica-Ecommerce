import { Component, Inject, OnInit } from '@angular/core';
import {MatDialog, MAT_DIALOG_DATA,MatDialogRef} from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ICategory } from 'src/app/Models/icategories';
import { IProduct } from 'src/app/Models/iproduct';
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
export class DialogComponent implements OnInit {
  categories? : ICategory[];
  colorArray: string[];

  constructor(@Inject(MAT_DIALOG_DATA) public data: IProduct,
  private catService: CategoryService, private productsService: ProductsService
  ,private router: Router) {
    this.colorArray = ["Blue", "Green", "Yellow", "Brown", "Pink", "Red"];

    // console.log("data = " + data.images);
  }

  ngOnInit(): void {
    this.catService.getAllCategories().subscribe((response)=>{
      this.categories = response;
    })
  }

  // onUpdate(){
  //   var files = this.imageInput.nativeElement.files;
  //   if (files.length === 0) {
  //     return;
  //   }
  //   let filesToUpload: File[] = files;
  //   const formData = new FormData();


  //   for (let i = 0; i < filesToUpload.length; i++) {
  //     formData.append("files", filesToUpload[i]);
  //   }
  //   formData.append("Title_EN", `${this.productToAdd.title_EN}`);
  //   formData.append("Title_AR", `${this.productToAdd.title_AR}`);
  //   formData.append("Details_EN", `${this.productToAdd.details_EN}`);
  //   formData.append("Details_AR", `${this.productToAdd.details_AR}`);
  //   formData.append("price", `${this.productToAdd.price}`);
  //   formData.append("Color", `${this.productToAdd.color}`);
  //   formData.append("Quantity", `${this.productToAdd.quantity}`);
  //   formData.append("CategoryID", `${this.productToAdd.categoryID}`);
  //   formData.append("OwnerId", `${this.productToAdd.ownerId}`);

  //   this.prdService.addProduct(formData).subscribe(observer)
  //   this.productsService.updateProduct(this.data.id, this.data)
  // }
  private mappingFunction(product: IProduct): ProductToAdd{
    var productToAdd = new ProductToAdd();
   
    /// Mapping !
    productToAdd.id = product?.id;
    console.log(product?.category)
    
    productToAdd.categoryID = this.categories?.find(c => c.name == product?.category)?.id ?? 1
    console.log(productToAdd.categoryID )

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
    var p = this.mappingFunction(this.data);
    this.productsService.updateProduct(this.data.id??0, p).subscribe((response)=>{
      console.log("Updated Successfully")

    },(error)=>{
      console.log(error)
    })
  }

  func(cat:string){
    return this.categories?.find(c=> c.name==cat)?.id ?? 0
  }
}
