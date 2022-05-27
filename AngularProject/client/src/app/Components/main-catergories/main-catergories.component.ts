import { Component, OnInit } from '@angular/core';
import { ICategory } from 'src/app/Models/icategories';
import { CategoryService } from 'src/app/Services/category.service';

@Component({
  selector: 'app-main-catergories',
  templateUrl: './main-catergories.component.html',
  styleUrls: ['./main-catergories.component.css']
})
export class MainCatergoriesComponent implements OnInit {

  categories: ICategory[] = [];

  constructor(private categoryService: CategoryService) {
  }

  ngOnInit(): void {
    this.categoryService.getAllCategories()
      .subscribe(categories => {
        this.categories = categories;
      });
  }

}
