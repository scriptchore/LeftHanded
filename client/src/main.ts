import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import 'zone.js';
import { register as registerSwiperElements } from 'swiper/element/bundle';


import { AppModule } from './app/app.module';

registerSwiperElements();
platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
