import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { CookieService } from 'ngx-cookie-service';
import { Emitters } from '../emitters/emitters';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  message:string = "You are not logged in";
  user:string = `${this.cookieService.get('username')}`;
  products:any;

  constructor(
    private http:HttpClient,
    private cookieService: CookieService
  ){};

  ngOnInit(): void {
    console.log(this.user);
    if(this.user!='' || this.user!=null)
    {
      this.http.get("https://localhost:44300/e-auction/api/v1/seller/get-product")
        .subscribe(res=>{
          this.products = res;
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
}
