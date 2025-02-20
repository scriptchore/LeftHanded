import { AfterViewInit, Component, CUSTOM_ELEMENTS_SCHEMA, OnInit, ViewEncapsulation } from '@angular/core';


@Component({
  selector: 'app-index',
  standalone: true,
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss'],

  schemas: [CUSTOM_ELEMENTS_SCHEMA] // Allows using custom elements like Swiper
})
export class IndexComponent implements AfterViewInit{
  ngAfterViewInit(): void {
    
    const swiperEl: any = document.querySelector('swiper-container');
    swiperEl.swiper.autoplay.start();


    swiperEl.addEventListener('autoplayStart', () => {
      console.log('Autoplay started');
    });
    swiperEl.addEventListener('autoplayStop', () => {
      console.log('Autoplay stopped');
    });
  }
}

 


