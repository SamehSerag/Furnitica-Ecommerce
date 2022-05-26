import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {
  cartItemcount$ = new Subject<number>();
}