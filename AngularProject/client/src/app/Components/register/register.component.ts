import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { RegisterDTO } from 'src/app/DTOs/RegisterDTO';
import { Gender } from 'src/app/Enums/Gender';
import { ILoggedInUser } from 'src/app/Models/ILoggedInUser';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit, OnDestroy {

  constructor(private auth : AuthService, private router : Router) { }

  confirmPassword : string = "";
  user! : RegisterDTO;
  sub! : Subscription;
  loading : boolean = false;
  validated : boolean = false;

  ngOnInit(): void {
    // this.user.username = "mohamedalaa";
    this.loading = false;
    this.user = {username : "", email : "", address : "", gender : Gender.undefined, password : "", role : ""}
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  isValidPassword(password : string) : boolean {
    let containUpper : boolean = false;
    let containLower : boolean = false;
    let containDigit : boolean = false;
    let containSpecial : boolean = false;
    for(let i = 0; i < password.length; i++) {
      let ch = password.charAt(i);
      if(ch >= 'a' && ch <= 'z')
        containLower = true;
      else if(ch >= 'A' && ch <= 'Z')
        containUpper = true;
      else if(ch >= '0' && ch <= '9')
        containDigit = true;
      else
        containSpecial = true;
    }
    return containLower && containUpper && containDigit && containSpecial && password.length >= 8;

  }

  isvalidEmail(email: string) : boolean {
    if(email.match(/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/))
      return true;
    return false;
  }

  isValid() : boolean {
    let res : boolean = ((this.user.username.length > 0)
              && (this.user.email.length > 0)
              && (this.user.address.length > 0)
              && (this.user.gender != null)
              && (this.isvalidEmail(this.user.email))
              && this.isValidPassword(this.user.password)
              && (this.user.password == this.confirmPassword));
    return res;
  }

  Register (roleName : string) {
    this.loading = true;
    this.validated = true;
    this.user.role = roleName;

    if(!this.isValid()) {
      console.log("invalid user data", this.user);
      this.loading = false;
      return;
    }

    console.log("loading = ", this.loading);
    console.log("userdata = ", this.user);
    this.sub = this.auth.Register(this.user).subscribe({
      next: res => {
        if(res["status"] == 201) {
          let loggedUser : ILoggedInUser = JSON.parse(JSON.stringify(res["body"]));
          localStorage.setItem("access-token", loggedUser.token);
          localStorage.setItem("user-data", JSON.stringify(loggedUser.userData));
          localStorage.setItem("expiration", loggedUser.expiration);
          this.router.navigate([""]);
        }
/*
        console.log(res);
        console.log("token = " + res.token);
        console.log(localStorage.getItem("access-token"));
        console.log(localStorage.getItem("user-data"));
        let obj = JSON.parse(localStorage.getItem("user-data")!);
        console.log(obj["username"]);
*/
      },
      error: err => {
        this.loading = false;
        console.log(err);
      },
      complete: ()=>{
        this.loading = false;
        console.log("request completed")
      }
    });

  }

}
