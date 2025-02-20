import { Component, HostListener, ViewEncapsulation } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { BasketItem } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class NavigationBarComponent {

viewCategory: any;
/* viewSubNav: any;
viewSuberNav: any; */
viewApp: any;

viewAd: any;
isMobile: boolean = false;
  showMobileNav: boolean = false;
  showCategory: boolean = false;
  viewSubNav: string | null = null;
  viewSuberNav: string | null = null;

@HostListener('window:resize', [])
onResize() {
  this.isMobile = window.innerWidth <= 768;
}

toggleMobileNav() {
  this.showMobileNav = !this.showMobileNav;
}

toggleCategory() {
  this.showCategory = !this.showCategory;
}

toggleSubNav(navId: string) {
  this.viewSubNav = this.viewSubNav === navId ? null : navId;
}

toggleSuberNav(navId: string) {
  this.viewSuberNav = this.viewSuberNav === navId ? null : navId;
}
isMobileNavOpen = false;



  constructor(public basketService: BasketService, public accountService: AccountService) {}

  getCount(items: BasketItem[]) {
    return items.reduce((sum, item) => sum + item.quantity, 0);
  }


}
