import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TechnologyPageComponent } from './pages/technology-page.component';

const routes: Routes = [
  { path: '', component: TechnologyPageComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TechnologyRoutingModule { }
