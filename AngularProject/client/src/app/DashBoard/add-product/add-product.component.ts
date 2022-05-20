import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ICategory } from 'src/app/Models/icategories';
import { IProduct } from 'src/app/Models/iproduct';
import { IOwnerProduct } from 'src/app/Models/Owner/IOwnerProduct';
import { IProductToAdd } from 'src/app/Models/Owner/IProductToAdd';
import { ProductToAdd } from 'src/app/Models/Owner/ProductToAdd';
import { CategoryService } from 'src/app/Services/category.service';
import { ProductsService } from 'src/app/Services/products.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  productToAdd!: ProductToAdd;
  categoriesList!: ICategory[];

  constructor(private categoryService:CategoryService, private prdService: ProductsService,
    private router:Router) { 
      this.productToAdd = new ProductToAdd();
      this.productToAdd.ownerId = "1";
      this.productToAdd.categoryID = 1;

      // this.categoryService.getAllCategories().subscribe((response)=>{
      //   this.categoriesList = response;
      //   console.log(this.categoriesList)
      // });
    }

  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe((response)=>{
      this.categoriesList = response;
      console.log(this.categoriesList)
    });
  }

  AddProduct() {
    const observer = {
      next: (prd: IOwnerProduct) => {
        alert("Added Successfully");
        // this.productsSearchModel.pageIndex = this.pagination.totalPages;
        // this.ngOnInit();
        // this.scrollToBottom();
        this.router.navigate([`/Products/${prd.id}`])

      },
      Error: (prd: IOwnerProduct) => {
        alert("Error !!");
      }

    }
    this.prdService.addProduct(this.productToAdd).subscribe(observer);
    // console.log(this.productToAdd);

  }

}
