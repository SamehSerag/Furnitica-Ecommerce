import { Gender } from "../Enums/Gender";

export interface IUser {
  userName : string;
  email : string;
  address : string;
  phoneNumber : String;
  gender : Gender;
  imageId : number;
}
