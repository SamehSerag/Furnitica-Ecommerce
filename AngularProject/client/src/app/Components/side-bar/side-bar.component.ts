import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IUser } from 'src/app/Models/IUser';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent implements OnInit {

  loggedIn : boolean = false;
  loggedUser : string = "";
  @ViewChild('searchInput') searchInput!: ElementRef; 
  constructor(private auth : AuthService, private router: Router) {
  }

  ngOnInit(): void {
    this.addUserDataSetItemHandler();
    this.updateState();
  }
  
  searhcQuery(){
    console.log(this.searchInput);
    this.router.navigate(
      ['/main-shop'],
      { queryParams: { search: `${this.searchInput.nativeElement.value}` } }
    );
  }

  updateState() {
    console.log("updating state..........");
    let userdata : string = localStorage.getItem("user-data") ?? "";

    if(userdata != "") {
      let user : IUser = JSON.parse(userdata);
      this.loggedIn = true;
      this.loggedUser = user.userName;
      console.log("Ligged In = ", this.loggedIn);
      console.log("user-data = " + userdata);
      console.log("user-data-obj = ", user);
      console.log("username = " + user.userName);
      console.log(this.loggedUser + " is logged in!");
    }
    else {
      this.loggedIn = false;
    }
  }

  addUserDataSetItemHandler() {
    const originalSetItem = localStorage.setItem;

    localStorage.setItem = function(key : string, value : string) {
      originalSetItem.apply(this, [key, value]);
      const event = new Event('userDataChanged');
      if(key == "user-data")
        document.dispatchEvent(event);
    };

    const userDataSetHandler = (e : Event) => {
      console.log("handling user-data set item event........");
      this.updateState();
    }

    document.addEventListener("userDataChanged", userDataSetHandler, false);
  }


  Logout() {
    console.log("logging out........");
    this.auth.Logout();
  }

}
