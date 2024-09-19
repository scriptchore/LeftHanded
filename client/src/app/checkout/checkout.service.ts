import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { envvironment } from 'src/environments/environment';
import { DeliveryMethod } from '../shared/models/deliveryMethods';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  baseUrl = envvironment.apiUrl;

  constructor(private http: HttpClient) { }


  getDeliveryMethods() {
    return this.http.get<DeliveryMethod[]>(this.baseUrl + 'orders/deliveryMethods').pipe(
      map(dm => {
        return dm.sort((a, b) => b.price - a.price) 
      })
    )
  }
}
