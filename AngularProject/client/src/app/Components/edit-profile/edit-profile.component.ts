import { Component, OnInit } from '@angular/core';
import { ILoggedInUser } from 'src/app/Models/ILoggedInUser';
import { IUser } from 'src/app/Models/IUser';
import { ProfileService } from 'src/app/Services/profile.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

  user? : IUser;
  editing : boolean = false;
  saving : boolean = false;
  errMessage : string = "all fields are required except phone Number";
  showMessage : boolean = false;

  constructor(private profile : ProfileService) { }

  ngOnInit(): void {
    let userdata : string = localStorage.getItem("user-data") ?? "";
    this.user = JSON.parse(userdata);
  }

  StartEditing() {
    this.editing = true;
  }

  closeMessage() {
    this.showMessage = false;
  }

  isvalidEmail(email: string) : boolean {
    if(email.match(/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/))
      return true;
    this.errMessage = "invalid email!"
    return false;
  }

  isValid() : boolean {
    let res : boolean = ((this.user!.userName.length > 0)
              && (this.user!.email.length > 0)
              && (this.user!.address.length > 0)
              && (this.isvalidEmail(this.user!.email)));

    return res;
  }

  Save() {

    if(!this.isValid()) {
      this.showMessage = true;
      return;
    }

    this.saving = true;
    this.profile.updateProfile(this.user!).subscribe({
      next : (res) => {
        if(res["status"] == 200) {
          let userdata : IUser = JSON.parse(JSON.stringify(res["body"]));
          localStorage.setItem("user-data", JSON.stringify(userdata));
          this.user = userdata;
        }
      },
      complete : () => {
        console.log("updated successfully!");
        this.saving = false;
        this.editing = false;
      },
      error: (err) => {
        this.errMessage = ProfileService.errMessage;
        this.showMessage = true;
        this.saving = false;
      }
    })
  }

}
