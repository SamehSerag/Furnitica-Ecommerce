import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
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
  textAr: boolean = false;
  loggedIn : boolean = false;
  loggedUser : string = "";
    IsEnglish:boolean=true;
    isAdmin: boolean = false;

  @ViewChild('searchInput') searchInput!: ElementRef;
  constructor(private auth : AuthService, private router: Router,public translate: TranslateService) {
    translate.addLangs(['en', 'nl']);
    translate.setDefaultLang('en');
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
    }
    else {
      this.loggedIn = false;
    }
    this.auth.isAdmin().subscribe({
      next: (res) =>{
        this.isAdmin = true;
        console.log("ressssssssssssssssssssssssssssssssssssssssssss", res);

      },
      error: (e)=>{
        this.isAdmin = false;
        console.log("errorrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr", e);
      },
    })
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
    debugger
    this.translate.use(lang);
    if (lang == 'nl') {
      this.textEn = false;
      this.textAr = true;
      this.translate.use("nl");
      //  this.setSession("lang", "ar");
      document.getElementById("arabicBoostrap")?.setAttribute('href', 'assets/LangFolders/r.css');
      document.getElementById("en")?.setAttribute('href', '');
      localStorage.removeItem('Lang');
      localStorage.setItem("Lang","ar");
      window.location.reload();

    } else {
      this.textEn = true;
      this.textAr = false;
      this.translate.use("en");
      //  this.setSession("lang", "en");
      document.getElementById("arabicBoostrap")?.setAttribute('href', '');

      document.getElementById("en")?.setAttribute('href', 'assets/LangFolders/l.css');
      localStorage.removeItem("Lang");

      localStorage.setItem("Lang","en");
      window.location.reload();
    }
  }


}

