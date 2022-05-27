import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
@Injectable({
  providedIn: 'root'
})
export class LanguagesChangeService {
  textEn: boolean = true;
  textAr: boolean=false;
  constructor(public http: HttpClient, private translate: TranslateService) { }
  /////////////////////////////////////
  useLanguage(language: string): void {
    /// get lang from localstorage
    if (language == 'ar') {
      this.textEn = false;
      this.textAr = true;
      this.translate.use('ar');
      document.getElementById("arabicBoostrap")?.setAttribute('href', 'assets/LangFolders/bootstrap-rtl.css');
      document.getElementById("en")?.setAttribute('href', '');
    } else {
      this.textEn = true;
      this.textAr = false;
      this.translate.use('en');
      document.getElementById("arabicBoostrap")?.setAttribute('href', '');
      // document.getElementById("en").setAttribute('href', 'assets/css/bootstrap.css');
    }
  }

  //=====================================================================
  switshLanguage(lang:string) {

    if (lang == 'ar') {
      this.textEn = false;
      this.textAr = true;
      this.translate.use("ar");
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
