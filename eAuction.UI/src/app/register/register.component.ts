import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { HttpClient } from '@angular/common/http'
import { Router } from '@angular/router';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent{
  profileForm = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    address:new FormControl(''),
    city:new FormControl(''),
    state:new FormControl(''),
    pin:new FormControl(''),
    phone:new FormControl(''),
    email:new FormControl(''),
    password:new FormControl('')
  });

  constructor(
    private http:HttpClient,
    private router:Router
  ){};

  onSubmit() {
    // TODO: Use EventEmitter with form value
    this.http.post("https://localhost:5001/Account/CreateUser", this.profileForm.value)
    .subscribe(()=>{this.router.navigate(['/login'])});
  }
}
