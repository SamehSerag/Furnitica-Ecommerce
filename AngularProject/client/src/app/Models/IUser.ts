import { Gender } from "../Enums/Gender";

export interface IUser {
  userId: number;
  username : string;
  email : string;
  gender : Gender;
  address : string;
}
