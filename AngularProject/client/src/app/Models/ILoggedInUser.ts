import { IUser } from "./IUser";

export interface ILoggedInUser {
  userData : IUser;
  token : string;
  expiration: string;
}
