import { Gender } from "../Enums/Gender";

export interface RegisterDTO {

  username : string;
  password : string ;
  email : string ;
  gender : Gender;
  address : string;
  role : string;
}
