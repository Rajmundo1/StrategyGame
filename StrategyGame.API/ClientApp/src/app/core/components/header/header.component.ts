import { Component, OnInit } from '@angular/core';

import { GameService } from '../../services/game.service';
import { tap, catchError } from 'rxjs/operators';
import { LayoutComponent } from '../layout/layout.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from 'src/environments/environment';
import { RefreshDataService } from '../../services/refresh-data.service';
import { of, Observable } from 'rxjs';
import { MainPageDto } from 'src/app/shared/clients';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  mainPageData: MainPageDto;
  countyId: string;

  baseUrl = environment.apiUrl;

  constructor(
    private gameService: GameService,
    private authService: AuthService,
    private snackbar: MatSnackBar,
    public refreshService: RefreshDataService,
    private router: Router
    ) { }

  ngOnInit(): void {

    this.getData();
    this.refreshService.data.subscribe(res => {
      this.getData();
    });
  }

  getData(): void{
    console.log("header.getdata() called with param: " + this.countyId);
    if(this.countyId){
      this.gameService.getCountyPage(this.countyId).pipe(
        catchError(this.handleError<MainPageDto>('Main page data loading was unsuccessful'))
      ).subscribe(res => {
        this.mainPageData = res;
        console.log("header mainpage data(" + this.countyId + ")");
        console.log(this.mainPageData);
      });
    }
    else{
      this.gameService.getMainPage().pipe(
        catchError(this.handleError<MainPageDto>('Main page data loading was unsuccessful'))
      ).subscribe(res => {
        this.mainPageData = res;
        console.log("header mainpage data(no countyID)");
        console.log(this.mainPageData);
      });
    }
  }

  newRound(): void{
    this.gameService.newRound(
    ).pipe(
      tap(res => {
        this.snackbar.open('New Round', 'Close', {
          duration: 3000,
          panelClass: ['my-snackbar'],
        });
        this.refreshService.refresh(true);
      }),
      catchError(this.handleError('Error while stepping round'))
    ).subscribe();
  }

  logout(): void {
    this.authService.logout()
    .subscribe(_ => this.router.navigate(['login']));
  }

  scoreboard(): void{
    this.router.navigate(['scoreboard']);
  }

  counties(): void{
    let id = this.mainPageData.currentKingdomId;
    this.router.navigate([`counties/${id}`]);
  }

  attacks(): void{
    let id = this.mainPageData.currentCountyId;
    this.router.navigate([`attack/${id}` ]);
  }

  technologies(): void{
    let id = this.mainPageData.currentKingdomId;
    this.router.navigate([`technologies/${id}`]);
  }

  units(): void{
    let id = this.mainPageData.currentCountyId;
    this.router.navigate([`units/${id}`]);
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
