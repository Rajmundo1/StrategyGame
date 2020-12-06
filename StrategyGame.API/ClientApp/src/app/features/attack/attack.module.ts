import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { AttackRoutingModule } from './attack-routing.module';
import { AttackComponent } from './components/attack.component';
import { AttackPageComponent } from './pages/attack.page.component';

@NgModule({
  declarations: [
    AttackPageComponent,
    AttackComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    AttackRoutingModule,
  ],
  exports: [
  ]
})
export class AttackModule { }
