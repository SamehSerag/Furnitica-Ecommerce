import { Component, OnInit } from '@angular/core';
import { IUser } from 'src/app/Models/IUser';
import { AuthService } from 'src/app/Services/auth.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent implements OnInit {
  textEn: boolean = true;
  textAr: boolean=false;
  loggedIn : boolean = false;
  loggedUser : string = "";

  constructor(private auth : AuthService, public translate: TranslateService) {
    translate.addLangs(['en', 'nl']);
    translate.setDefaultLang('en');
  }

  ngOnInit(): void {
    this.addUserDataSetItemHandler();
    this.updateState();
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

/**********************************************
 * 
 * Switching LAnguage
 * 
 * ****************************************** */
 switchLang(lang: string) {
  this.translate.use(lang);
  if (lang == 'nl') {
    this.textEn = false;
    this.textAr = true;
    this.translate.use("nl");
    //  this.setSession("lang", "ar");
    document.getElementById("arabicBoostrap")?.setAttribute('href', 'assets/LangFolders/bootstrap-rtl.css');
    document.getElementById("en")?.setAttribute('href', '');


  } else {
    this.textEn = true;
    this.textAr = false;
    this.translate.use("en");
    //  this.setSession("lang", "en");
    document.getElementById("arabicBoostrap")?.setAttribute('href', '');

    document.getElementById("en")?.setAttribute('href', 'assets/LangFolders/bootstrap.css');
  }
}


}
