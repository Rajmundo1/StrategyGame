import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { TechnologyPageComponent } from './pages/technology-page.component';
import { TechnologyRoutingModule } from './technology-routing.module';



@NgModule({
  declarations: [TechnologyPageComponent],
  imports: [
    CommonModule,
    SharedModule,
    TechnologyRoutingModule
  ]
})
export class TechnologyModule { }
