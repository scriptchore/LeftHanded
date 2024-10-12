import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { envvironment } from 'src/environments/environment';
import { Order } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  baseUrl = envvironment.apiUrl;


  constructor(private http: HttpClient) { }

  getOrdersForUser() {
    return this.http.get<Order[]>(this.baseUrl + 'orders')
  }

  getOrdersDetailed(id: number) {
    return this.http.get<Order>(this.baseUrl + 'orders/' + id)
  }
}
