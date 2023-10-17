import { Component, OnInit } from '@angular/core';
import { Emitters } from '../emitters/emitters';
import { HttpClient } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  authenticated:boolean=false;

  constructor(private http:HttpClient, private cookieService: CookieService){
    Emitters.authEmitter.subscribe((auth:boolean)=>{
      this.authenticated=auth;
    });
  }

  ngOnInit(): void {
    Emitters.authEmitter.subscribe((auth:boolean)=>{
      this.authenticated=auth;
    });
  }

  logout():void{
    this.http.post("http://localhost:32784/Account/Logout", {}, {withCredentials:true})
      .subscribe(res=>{
        this.authenticated=false;
        Emitters.authEmitter.emit(false);
        this.cookieService.delete('username');
        this.cookieService.delete('jwtToken');
      },
      err=>{
        Emitters.authEmitter.emit(false);
        this.cookieService.delete('username');
        this.cookieService.delete('jwtToken');
      });
  }
}
