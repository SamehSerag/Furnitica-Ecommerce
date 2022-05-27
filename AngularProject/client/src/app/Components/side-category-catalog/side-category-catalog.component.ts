import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ICategory } from 'src/app/Models/icategories';
import { CategoryService } from 'src/app/Services/category.service';

@Component({
  selector: 'app-side-category-catalog',
  templateUrl: './side-category-catalog.component.html',
  styleUrls: ['./side-category-catalog.component.css']
})
export class SideCategoryCatalogComponent implements OnInit {

  categories: ICategory[] = [];
  selectedCategories: number[] = [];

  @Output() ChangeCategory = new EventEmitter<number[]>();

  constructor(private categoryService: CategoryService, private activatedRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.categoryService.getAllCategories()
      .subscribe(categories => {
        this.categories = categories;
      }
      );

    if (this.activatedRoute.snapshot.paramMap.get('catId') != null)
      this.selectedCategories.push(Number(this.activatedRoute.snapshot.paramMap.get('catId')));
  }


  changeCat(id: number) {

    var i = this.selectedCategories.indexOf(id);

    if (i == -1) {
      this.selectedCategories.push(id);
    } else {
      var i = this.selectedCategories.indexOf(id);
      this.selectedCategories.splice(i,1);
    }
    this.ChangeCategory.emit(this.selectedCategories);
  }

  CatExist(id: number): boolean {
    return this.selectedCategories.indexOf(id) !=-1;
  }

}
