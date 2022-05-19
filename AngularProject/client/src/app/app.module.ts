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
import { ProductDetialsComponent } from './Components/product-detials/product-detials.component';
import { ReviewsComponent } from './Components/reviews/reviews.component';
import { ProductReviewsComponent } from './Components/product-reviews/product-reviews.component';
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
    ProductDetialsComponent,
    ReviewsComponent,
    ProductReviewsComponent,
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
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
// AOT compilation support
export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
