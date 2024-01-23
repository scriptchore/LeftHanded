import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
 
  title = 'LeftHanded';
  products: any[] = [];


  constructor(private http: HttpClient) {}


  ngOnInit(): void {
   this.http.get('https://localhost:5001/api/products?PageSize=50').subscribe({
    //next: response => console.log(response), //what to do next
    next: (response: any) => this.products = response.data,
    error: error => console.log(error), // what to do if there is an error
    complete: () => { 
      console.log('request completed');
      console.log('extra statement');
    }
   })
  }
}
