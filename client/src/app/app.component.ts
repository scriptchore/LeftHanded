import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
 
  title = 'LeftHanded';
  //products: any[] = [];



  constructor(private basketService: BasketService, private accountService: AccountService) {}


  ngOnInit(): void {
   this.loadBasket();
   this.loadCurrentUser();
  }

  loadBasket(){
    const basketID = localStorage.getItem('basket_id');
   if (basketID) this.basketService.getBasket(basketID)
  }


  loadCurrentUser(){
    const token = localStorage.getItem('token');
    if(token) this.accountService.loadCurrentUser(token).subscribe();
  }
}
