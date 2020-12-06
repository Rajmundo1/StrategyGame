import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule, MatButton} from '@angular/material/button';
import {MatSliderModule} from '@angular/material/slider';
import {MatListModule} from '@angular/material/list';
import {MatIconModule} from '@angular/material/icon';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

import {TableSearchComponent} from './components/table-search.component'
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    TableSearchComponent,
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatInputModule,
    MatDividerModule,
    MatButtonModule,
    MatSliderModule,
    MatListModule,
    MatIconModule,
    HttpClientModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
  ],
  exports: [
    MatCardModule,
    MatInputModule,
    MatDividerModule,
    MatButtonModule,
    MatSliderModule,
    MatListModule,
    TableSearchComponent,
    MatIconModule,
    HttpClientModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
  ]
})
export class SharedModule { }
