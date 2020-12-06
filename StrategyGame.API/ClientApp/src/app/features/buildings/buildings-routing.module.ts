import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { BuildingsPageComponent } from './pages/buildings.page.component';

const routes: Routes = [
  { path: '', component: BuildingsPageComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BuildingsRoutingModule { }
