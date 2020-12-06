import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './core/components/layout/layout.component';
import { HeaderComponent } from './core/components/header/header.component';
import { LoginComponent } from './core/pages/login/login.component';

import { AuthGuardService } from '../app/core/services/auth-guard.service';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', loadChildren: () => import('./core/core.module').then(m => m.CoreModule), },
      { path: 'mainPage/:countyId', loadChildren: () => import('./core/core.module').then(m => m.CoreModule), },
      { path: 'attack/:countyId', loadChildren: () => import('./features/attack/attack.module').then(m => m.AttackModule), },
      { path: 'building/:buildingId', loadChildren: () => import('./features/buildings/buildings.module').then(m => m.BuildingsModule), },
      { path: 'technologies/:kingdomId', loadChildren: () => import('./features/technology/technology.module').then(m => m.TechnologyModule), },
      { path: 'counties/:kingdomId', loadChildren: () => import('./features/county/county.module').then(m => m.CountyModule), },
      { path: 'units/:countyId', loadChildren: () => import('./features/unit/unit.module').then(m => m.UnitModule), },
      { path: 'scoreboard', loadChildren: () => import('./features/scoreboard/scoreboard.module').then(m => m.ScoreboardModule), },

    ],
    canActivate: [AuthGuardService]
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
