import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CitiesComponent } from './cities/cities.component';
import { ScoreboardComponent } from './scoreboard/scoreboard.component';
import { TechnologiesComponent } from './technologies/technologies.component';
import { BuildingsComponent } from './buildings/buildings.component';
import { UnitsComponent } from './units/units.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    CitiesComponent,
    ScoreboardComponent,
    TechnologiesComponent,
    BuildingsComponent,
    UnitsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'cities', component: CitiesComponent},
      { path: 'scoreboard', component: ScoreboardComponent},
      { path: 'technologies', component: TechnologiesComponent},
      { path: 'buildings', component: BuildingsComponent},
      { path: 'units', component: UnitsComponent}

    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
