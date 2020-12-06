import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { UnitRoutingModule } from './unit-routing.module';
import { UnitPageComponent } from './pages/unit-page.component';

@NgModule({
  declarations: [
    UnitPageComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    UnitRoutingModule,
  ],
  exports: [
  ]
})
export class UnitModule { }