import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ScoreboardRoutingModule } from './scoreboard-routing.module';
import { ScoreboardComponent } from './components/scoreboard.component';
import { ScoreboardPageComponent } from './pages/scoreboard.page.component';

@NgModule({
  declarations: [
    ScoreboardPageComponent,
    ScoreboardComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ScoreboardRoutingModule,
  ],
  exports: [
  ]
})
export class ScoreboardModule { }
