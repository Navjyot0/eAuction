import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { AddProductComponent } from './add-product/add-product.component';
import { MakeBidComponent } from './make-bid/make-bid.component';

const routes: Routes = [
  {path:'', component:HomeComponent},
  {path:'product-details/:productid', component:ProductDetailsComponent},
  {path:'add-product', component:AddProductComponent},
  {path:'make-bid/:productid', component:MakeBidComponent},
  {path:'register', component:RegisterComponent},
  {path:'login', component:LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
