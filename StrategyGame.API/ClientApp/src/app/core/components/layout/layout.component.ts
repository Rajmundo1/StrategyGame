import { Component, OnInit } from '@angular/core';

import { tap, catchError } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RefreshDataService } from '../../services/refresh-data.service';
import { of, Observable } from 'rxjs';
import { GameService } from '../../services/game.service';
import { MainPageDto } from 'src/app/shared/clients';
import { ActivatedRoute } from '@angular/router';
import { HeaderComponent } from '../header/header.component';


@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  mainPageData: MainPageDto;

  countyId: string;

  constructor(
    private gameService: GameService,
    private snackbar: MatSnackBar,
    private refreshService: RefreshDataService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getData();
    this.refreshService.data.subscribe(res => {
      this.getData();
    });
  }

  getData(): void{
    console.log("layout.getdata() called with param: " + this.countyId);
    if(this.countyId){
      this.gameService.getCountyPage(this.countyId).pipe(
        tap(res => this.mainPageData = res),
        catchError(this.handleError<MainPageDto>('Main page data loading was unsuccessful'))
      ).subscribe();
    }
    else{
      this.gameService.getMainPage().pipe(
        tap(res => this.mainPageData = res),
        catchError(this.handleError<MainPageDto>('Main page data loading was unsuccessful'))
      ).subscribe();
    }
  }

  private handleError<T>(message = 'Error', result?: T) {
    return (error: any): Observable<T> => {
      this.snackbar.open(message, 'Close', {
        duration: 3000,
        panelClass: ['my-snackbar'],
      });
      return of(result as T);
    };
  }
}
