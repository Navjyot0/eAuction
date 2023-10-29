import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { HttpClient } from '@angular/common/http'
import { Router } from '@angular/router';
import { Emitters } from '../emitters/emitters';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm = new FormGroup({
    UserName:new FormControl(''),
    Password:new FormControl('')
  });
  loginDetails:any;
  constructor(
    private http:HttpClient,
    private router:Router,
    private cookieService: CookieService
  ){};

  onSubmit() {
    
    // TODO: Use EventEmitter with form value
    this.http.post("http://3.80.238.167:30378/Account", this.loginForm.value, {withCredentials:true})
      .subscribe(res=>{
        this.loginDetails = res;
        this.router.navigate(['/']);
        console.log(JSON.stringify(res));
        this.cookieService.delete('username');
        this.cookieService.delete('jwtToken');
        this.cookieService.set('username', this.loginDetails.userName);
        this.cookieService.set('jwtToken', this.loginDetails.jwtToken);
        Emitters.authEmitter.emit(true);
      },
      err=>{
        Emitters.authEmitter.emit(false);
      }
      );
  }
}
