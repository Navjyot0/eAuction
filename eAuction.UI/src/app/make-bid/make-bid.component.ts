import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { HttpClient } from '@angular/common/http'
import { Router } from '@angular/router';
import { Emitters } from '../emitters/emitters';
import { CookieService } from 'ngx-cookie-service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-make-bid',
  templateUrl: './make-bid.component.html',
  styleUrls: ['./make-bid.component.css']
})
export class MakeBidComponent {
  bidForm = new FormGroup({
    productId: new FormControl(''),
    bidAmount:new FormControl(''),
    buyerEmailId:new FormControl('')
  });
  user:string = `${this.cookieService.get('username')}`;
  
  constructor(
    private http:HttpClient,
    private router:Router,
    private cookieService: CookieService,
    private route: ActivatedRoute
  ){
    this.bidForm.patchValue({
      buyerEmailId: `${this.cookieService.get('username')}`,
      productId: this.route.snapshot.paramMap.get('productid')
    });
  };

  onSubmit() {
    // TODO: Use EventEmitter with form value
    this.http.post("https://localhost:44300/e-auction/api/v1/buyer/place-bid", this.bidForm.value)
    .subscribe(res=>{
      Emitters.authEmitter.emit(true);
      this.router.navigate(['/product-details/'+this.bidForm.value.productId]);
    },
    err=>{
      console.log(err);
      Emitters.authEmitter.emit(false);
      this.cookieService.delete('username');
      this.cookieService.delete('jwtToken');
    });
  }
}
