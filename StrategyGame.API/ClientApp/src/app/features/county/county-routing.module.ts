import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CountyPageComponent } from '../county/page/county-page.component';

const routes: Routes = [
  { path: '', component: CountyPageComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CountyRoutingModule { }