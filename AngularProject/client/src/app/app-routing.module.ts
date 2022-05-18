import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditProfileComponent } from './Components/edit-profile/edit-profile.component';
import { HomeComponent } from './Components/home/home.component';
import { LoginComponent } from './Components/login/login.component';
import { MainShopComponent } from './Components/main-shop/main-shop.component';
import { OrdersComponent } from './Components/orders/orders.component';
import { RegisterComponent } from './Components/register/register.component';
import { AllOrdersComponent } from './DashBoard/all-orders/all-orders.component';
import { PendingOrdersComponent } from './DashBoard/pending-orders/pending-orders.component';
import { IndexComponent } from './index.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'main-shop', component:MainShopComponent },
  { path: 'edit-profile', component:EditProfileComponent},
  { path: 'Orders', component:OrdersComponent},
  { path: 'Orders/pending', component:PendingOrdersComponent},
  { path: 'Orders/allOrders', component:AllOrdersComponent},
  { path: 'Register', component:RegisterComponent},
  { path: 'Login', component:LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
