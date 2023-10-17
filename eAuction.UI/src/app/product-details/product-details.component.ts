import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Emitters } from '../emitters/emitters';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  id:any|null;
  message:string = "You are not logged in";
  user:string = `${this.cookieService.get('username')}`;

  constructor(private http:HttpClient, private route: ActivatedRoute, private cookieService: CookieService){}
  productDetails:any;

  ngOnInit(): void {
    this.getProductDetails();
  }

  getProductDetails(): void {
    this.id = this.route.snapshot.paramMap.get('productid');
    this.http.get("http://localhost:44397/e-auction/api/v1/seller/show-bids?productId="+this.id)
        .subscribe(res=>{
            this.productDetails = res;
            console.log(res);
            Emitters.authEmitter.emit(true);
          }, 
          err=>{
            console.log(err);
            Emitters.authEmitter.emit(false);
            this.cookieService.delete('username');
            this.cookieService.delete('jwtToken');
          });
  }
}
