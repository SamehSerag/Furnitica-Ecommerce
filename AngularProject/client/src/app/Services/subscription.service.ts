import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { IUser } from '../Models/IUser';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  ///////////// NEED TO GETT USERRR IDD
    //userData = new BehaviorSubject<IUser>(new IUser());
    searchItemValue$ = new BehaviorSubject<string>('');
    cartItemcount$ = new Subject<number>();
}