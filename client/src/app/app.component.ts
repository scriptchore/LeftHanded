import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from './models/products';
import { Pagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
 
  title = 'LeftHanded';
  //products: any[] = [];
  products: Product[] = [];


  constructor(private http: HttpClient) {}


  ngOnInit(): void {
   this.http.get<Pagination<Product[]>>('https://localhost:5001/api/products?PageSize=50').subscribe({
    //next: response => console.log(response), //what to do next
    next: response => this.products = response.data,
    error: error => console.log(error), // what to do if there is an error
    complete: () => { 
      console.log('request completed');
      console.log('extra statement');
    }
   })
  }
}
