import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { HttpClient } from '@angular/common/http'
import { Router } from '@angular/router';
import { Emitters } from '../emitters/emitters';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent {
  productForm = new FormGroup({
    productName: new FormControl(''),
    shortDescription:new FormControl(''),
    detailedDescription:new FormControl(''),
    category:new FormControl(''),
    startingPrice:new FormControl(''),
    bidEndDate:new FormControl(''),
    sellerEmailId: new FormControl('')
  });

  constructor(
    private http:HttpClient,
    private router:Router,
    private cookieService: CookieService
  ){
    this.productForm.patchValue({
      sellerEmailId: `${this.cookieService.get('username')}`
    });
  };

  onSubmit() {
    // TODO: Use EventEmitter with form value
    // this.http.post("http://localhost:44397/e-auction/api/v1/seller/add-product", this.productForm.value)
    this.http.post("http://localhost:30397/api/Product/AddProduct", this.productForm.value)
    .subscribe(res=>{
      Emitters.authEmitter.emit(true);
      this.router.navigate(['/']);
    },
    err=>{
      console.log(err);
      Emitters.authEmitter.emit(false);
      this.cookieService.delete('username');
      this.cookieService.delete('jwtToken');
    });
  }
}
