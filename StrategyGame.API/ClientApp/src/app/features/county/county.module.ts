import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { CountyRoutingModule } from './county-routing.module';
import { CountyPageComponent } from '../county/page/county-page.component';

@NgModule({
  declarations: [
    CountyPageComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    CountyRoutingModule,
  ],
  exports: [
  ],
  providers: [
    
  ]
})
export class CountyModule { }