import { NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { SharedModule } from '../shared/shared.module';
import { IndexComponent } from './index/index.component';





@NgModule({
  declarations: [
    HomeComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    IndexComponent
],
  exports:[
    HomeComponent
  ]
})
export class HomeModule { }
