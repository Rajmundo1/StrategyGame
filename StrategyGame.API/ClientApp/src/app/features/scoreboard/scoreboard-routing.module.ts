import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ScoreboardPageComponent } from './pages/scoreboard.page.component';

const routes: Routes = [
  { path: '', component: ScoreboardPageComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ScoreboardRoutingModule { }
