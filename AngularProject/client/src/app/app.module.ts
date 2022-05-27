import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { SideBarComponent } from './Components/side-bar/side-bar.component';
import { FooterComponent } from './Components/footer/footer.component';
import { AppRoutingModule } from './app-routing.module';
import { IndexComponent } from './index.component';
import { MainComponent } from './DashBoard/main.component';
import { HomeComponent } from './Components/home/home.component';
import { MainShopComponent } from './Components/main-shop/main-shop.component';
import { AsListPipe } from './Pipes/as-list.pipe';
import { MainCatergoriesComponent } from './Components/main-catergories/main-catergories.component';
import { EditProfileComponent } from './Components/edit-profile/edit-profile.component';
import { RegisterComponent } from './Components/register/register.component';
import { LoginComponent } from './Components/login/login.component';
import { FormsModule } from '@angular/forms';
import { OrdersComponent } from './Components/orders/orders.component';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { PendingOrdersComponent } from './DashBoard/pending-orders/pending-orders.component';
import { AllOrdersComponent } from './DashBoard/all-orders/all-orders.component';
import { ProductDetialsComponent } from './Components/product-detials/product-detials.component';
import { ProductReviewsComponent } from './Components/product-reviews/product-reviews.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProductListComponent } from './DashBoard/product-list/product-list.component';
import { PipeTransformPipe } from './Pipes/pipe-transform.pipe';
import { AddProductComponent } from './DashBoard/add-product/add-product.component';
import { MatSliderModule } from '@angular/material/slider';
import { DialogComponent } from './DashBoard/dialog/dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatButtonModule} from '@angular/material/button';
import { NgxSliderModule } from '@angular-slider/ngx-slider';
import { SideCategoryCatalogComponent } from './Components/side-category-catalog/side-category-catalog.component';
import { WishlistComponent } from './Components/wishlist/wishlist.component';
import { WishlistHeartComponent } from './Components/wishlist-heart/wishlist-heart.component';
import { AddtocartComponent } from './Components/addtocart/addtocart.component';
import { ShoppingcartComponent } from './Components/shoppingcart/shoppingcart.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CheckOutComponent } from './Components/check-out/check-out.component';

@NgModule({
  declarations: [
    AppComponent,
    SideBarComponent,
    FooterComponent,
    IndexComponent,
    MainComponent,
    HomeComponent,
    MainShopComponent,
    AsListPipe,
    MainCatergoriesComponent,
    EditProfileComponent,
    RegisterComponent,
    LoginComponent,
    OrdersComponent,
    PendingOrdersComponent,
    ProductReviewsComponent,
    AllOrdersComponent,
    ProductDetialsComponent,
    ProductListComponent,
    PipeTransformPipe,
    AddProductComponent,
    DialogComponent,
    SideCategoryCatalogComponent,
    WishlistComponent,
    WishlistHeartComponent,
    AddtocartComponent,
    ShoppingcartComponent,
    CheckOutComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,

    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: httpTranslateLoader,
        deps: [HttpClient]
      }
    }),
        AppRoutingModule,
        FormsModule,
        BrowserAnimationsModule,
        AccordionModule,
        MatSliderModule,
        MatDialogModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatButtonModule,
        NgxSliderModule,
        ReactiveFormsModule
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
// AOT compilation support
export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
