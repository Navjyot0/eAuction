import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  message:string = "You are not logged in";
  user:string='';

  constructor(
    private http:HttpClient,
    private cookieService: CookieService
  ){};

  ngOnInit(): void {
    this.http.get("https://localhost:44300/e-auction/api/v1/seller/get-product")
      .subscribe(res=>{
        this.user =  `${this.cookieService.get('username')}`;
        console.log(res)}, 
        err=>{
          console.log(err)
        });
  }
}
