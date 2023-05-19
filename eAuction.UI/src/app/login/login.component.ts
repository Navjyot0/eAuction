import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { HttpClient } from '@angular/common/http'
import { Router } from '@angular/router';
import { Emitters } from '../emitters/emitters';

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

  constructor(
    private http:HttpClient,
    private router:Router
  ){};

  onSubmit() {
    // TODO: Use EventEmitter with form value
    this.http.post("https://localhost:44300/Account", this.loginForm.value, {withCredentials:true})
      .subscribe(res=>{
        this.router.navigate(['/']);
        Emitters.authEmitter.emit(true);
      },
      err=>{
        Emitters.authEmitter.emit(false);
      }
      );
  }
}
