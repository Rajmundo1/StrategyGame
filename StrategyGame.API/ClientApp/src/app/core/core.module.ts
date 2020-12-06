import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import { SharedModule } from '../shared/shared.module';
import { CoreRoutingModule } from './core-routing.module';
import { AuthService } from './services/auth.service';
import { LoginComponent } from './pages/login/login.component';

import { HeaderComponent } from './components/header/header.component';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { LayoutComponent } from './components/layout/layout.component';
import { MatDialogModule } from '@angular/material/dialog';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import { environment } from 'src/environments/environment';
import { API_BASE_URL } from '../shared/clients';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { AuthGuardService } from './services/auth-guard.service';
import { SignalRService } from './services/signal-r.service';
import { GameService } from './services/game.service';
import { RefreshDataService } from './services/refresh-data.service';

@NgModule({
  declarations: [
    LoginComponent,
    LayoutComponent,
    HeaderComponent,
    MainPageComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    CoreRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatSidenavModule,
    MatToolbarModule,
    MatButtonModule,
  ],
  exports: [
    SharedModule
  ],
  providers: [
    AuthGuardService,
    AuthService,
    GameService,
    RefreshDataService,
    SignalRService,
    { provide: API_BASE_URL, useValue: environment.apiUrl },
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
  ]
})
export class CoreModule { }
