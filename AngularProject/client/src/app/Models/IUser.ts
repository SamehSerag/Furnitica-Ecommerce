import { Gender } from "../Enums/Gender";

export interface IUser {
  username : string;
  email : string;
  gender : Gender;
  address : string;
}
