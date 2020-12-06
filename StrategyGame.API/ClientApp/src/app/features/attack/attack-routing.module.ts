import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AttackPageComponent } from './pages/attack.page.component';


const routes: Routes = [
  { path: '', component: AttackPageComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AttackRoutingModule { }
