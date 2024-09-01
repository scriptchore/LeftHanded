import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
 
  title = 'LeftHanded';
  //products: any[] = [];



  constructor(private basketService: BasketService) {}


  ngOnInit(): void {
   const basketID = localStorage.getItem('basket_id');
   if (basketID) this.basketService.getBasket(basketID)
  }
}
