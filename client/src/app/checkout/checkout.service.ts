import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { DeliveryMethod } from '../shared/models/deliveryMethods';
import { map, of, ReplaySubject } from 'rxjs';
import { User } from '../shared/models/user';
import { Order, OrderToCreate } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  private currentUserSource = new ReplaySubject<User | null>(1);

  currentUser$ = this.currentUserSource.asObservable();

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  createOrder(order: OrderToCreate) {
    return this.http.post<Order>(this.baseUrl + 'orders', order)
  }


  getDeliveryMethods() {

    /* if(token === null){
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();

    headers = headers.set('Authorization', `Bearer ${token}`); */


    return this.http.get<DeliveryMethod[]>(this.baseUrl + 'orders/deliveryMethods').pipe(
      map(dm => {
        return dm.sort((a, b) => b.price - a.price) 
      })
    )
  }
}
