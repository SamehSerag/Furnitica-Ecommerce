import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { LoginDTO } from 'src/app/DTOs/LoginDTO';
import { Gender } from 'src/app/Enums/Gender';
import { ILoggedInUser } from 'src/app/Models/ILoggedInUser';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  constructor(private auth : AuthService, private router : Router) { }

  confirmPassword : string = "";
  user! : LoginDTO;
  sub! : Subscription;
  loading : boolean = false;
  validated : boolean = false;
  showMessage : boolean  = false;
  errMessage : string = "default err message";


  ngOnInit(): void {
    // this.user.username = "mohamedalaa";
    this.loading = false;
    this.user = {username : "", password : ""}
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }


  isValid() : boolean {
    return this.user.username.length > 0 && this.user.password.length > 0;
  }

  closeMessage() {
    this.showMessage = false;
  }

  Login () {
    this.loading = true;
    this.validated = true;
    this.showMessage = false;
    if(!this.isValid()) {
      this.loading = false;
      return;
    }

    console.log("loading = ", this.loading);
    console.log("userdata = ", this.user);

    this.sub = this.auth.Login(this.user).subscribe({
      next: res => {
        if(res["status"] == 200) {
          let loggedUser : ILoggedInUser = JSON.parse(JSON.stringify(res["body"]));
          localStorage.setItem("access-token", loggedUser.token);
          localStorage.setItem("user-data", JSON.stringify(loggedUser.userData));
          localStorage.setItem("expiration", loggedUser.expiration);

          this.router.navigate([""]);
        }
      },
      error: err => {
        this.loading = false;
        this.showMessage = true;
        this.errMessage = AuthService.errMessage;
        console.log("auth-mess" +AuthService.errMessage);
        console.log("err = ", err);
      },
      complete: ()=>{
        this.loading = false;
        console.log("request completed")
      }
    });
  }
}
