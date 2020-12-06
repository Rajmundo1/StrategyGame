import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UnitPageComponent } from './pages/unit-page.component'

const routes: Routes = [
  { path: '', component: UnitPageComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UnitRoutingModule { }