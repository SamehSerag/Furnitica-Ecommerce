import { HttpClient, HttpEventType } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import { Router } from '@angular/router';
import { ICategory } from 'src/app/Models/icategories';
// import { IProduct } from 'src/app/Models/iproduct';
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
  progress: number = 0;
  message: string = "";
  productToAdd!: ProductToAdd;
  categoriesList!: ICategory[];
  @ViewChild('imageInput') imageInput!: ElementRef;
  onUploadFinished: any;
  colorArray: string[];

  isValidated : boolean = false;

  constructor(private categoryService: CategoryService, private prdService: ProductsService,
    private router: Router, private http: HttpClient) {
      this.colorArray = ["Blue", "Green", "Yellow", "Brown", "Pink", "Red"];

    this.productToAdd = new ProductToAdd();
    this.productToAdd.ownerId = "1";
    this.productToAdd.categoryID = 1;
    this.productToAdd.color = 1;
    this.productToAdd.details_AR = "";
    this.productToAdd.details_EN = "";
    this.productToAdd.title_AR = "";
    this.productToAdd.title_EN = "";
    this.productToAdd.quantity = 0;
    this.productToAdd.price = 0;
    console.log("product to add: ", this.productToAdd);
  }

  validate() : boolean{
    this.isValidated = true;
    return this.productToAdd.title_AR.length > 0
    && this.productToAdd.title_EN.length > 0
    && this.productToAdd.details_AR.length > 0
    && this.productToAdd.details_EN.length > 0
    && this.productToAdd.price > 0
    && this.productToAdd.quantity >= 0
    && this.imageInput.nativeElement.files.length !== 0;
  }


  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe((response) => {
      this.categoriesList = response;
      // console.log(this.categoriesList)
    });
  }

  AddProduct() {
    if(!this.validate())
      return;

    const observer = {
      next: (prd: IOwnerProduct) => {
        alert("Added Successfully");
        this.router.navigate([`/Products/${prd.id}`])

      },
      Error: (prd: IOwnerProduct) => {
        alert("Error !!");
      }

    }

    var files = this.imageInput.nativeElement.files;
    if (files.length === 0) {
      return;
    }
    let filesToUpload: File[] = files;
    const formData = new FormData();


    for (let i = 0; i < filesToUpload.length; i++) {
      formData.append("files", filesToUpload[i]);
    }
    formData.append("Title_EN", `${this.productToAdd.title_EN}`);
    formData.append("Title_AR", `${this.productToAdd.title_AR}`);
    formData.append("Details_EN", `${this.productToAdd.details_EN}`);
    formData.append("Details_AR", `${this.productToAdd.details_AR}`);
    formData.append("price", `${this.productToAdd.price}`);
    formData.append("Color", `${this.productToAdd.color}`);
    formData.append("Quantity", `${this.productToAdd.quantity}`);
    formData.append("CategoryID", `${this.productToAdd.categoryID}`);
    formData.append("OwnerId", `${this.productToAdd.ownerId}`);

    this.prdService.addProduct(formData).subscribe(observer)
  }

  // console.log(this.imageInput.nativeElement.value);
  // console.log(this.imageInput.nativeElement.files);
  // this.uploadFile(this.imageInput.nativeElement.files)

}

  // uploadFile = (files: any) => {
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

  //   this.prdService.addProduct(formData).subscribe((next)=>{
  //     console.log("ADD");
  //   }, (err)=>{
  //     console.log(err);
  //   })
  // }


/// Works Fine (TestDto)
// uploadFile = (files: any) => {
//     if (files.length === 0) {
//       return;
//     }
//     let filesToUpload: File[] = files;
//     const formData = new FormData();


//     for (let i = 0; i < filesToUpload.length; i++) {
//       formData.append("files", filesToUpload[i]);
//     }
//     formData.append("price", `${1000}`);
//     formData.append("name", `${this.productToAdd.title_EN}`);
//     this.http.post('https://localhost:44376/api/images/AddImages2', formData)
//       .subscribe((next) => {
//         console.log("Succeed")
//       }, (error) => {
//         console.log("ERROR");
//       });
//   }


// }
