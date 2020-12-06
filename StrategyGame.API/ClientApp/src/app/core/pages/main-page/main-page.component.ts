import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/internal/operators/tap';
import { catchError } from 'rxjs/operators';
import { BuildingDetailDto, BuildingDto, BuildingViewDto, MainPageDto } from 'src/app/shared/clients';
import { environment } from 'src/environments/environment';
import { HeaderComponent } from '../../components/header/header.component';
import { LayoutComponent } from '../../components/layout/layout.component';
import { GameService } from '../../services/game.service';
import { RefreshDataService } from '../../services/refresh-data.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent implements OnInit {

  baseUrl = environment.apiUrl;

  mainPageData: MainPageDto;

  countyIdParam: string;

  constructor(public layout: LayoutComponent,
              public header: HeaderComponent,
              private router: Router,
              private refreshService: RefreshDataService,
              private gameService: GameService,
              private snackbar: MatSnackBar,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.countyIdParam = this.route.snapshot.paramMap.get('countyId');
    console.log("mainpagecomponent called with param:  " + this.countyIdParam);

    this.layout.countyId = this.countyIdParam;
    this.header.countyId = this.countyIdParam;

    this.getData();
    this.layout.getData();
    this.header.getData();
    this.refreshService.data.subscribe(res => {
      this.getData();
      this.layout.getData();
    });

    this.header.refreshService.data.subscribe(res =>{
      this.header.getData();
    });
  }

  private getData(){
    console.log("mainpage.getdata() called with param: " + this.countyIdParam);
    if(this.countyIdParam){
      this.gameService.getCountyPage(this.countyIdParam).pipe(
        tap(res => this.mainPageData = res),
        catchError(this.handleError<MainPageDto>('Main page data loading was unsuccessful'))
      ).subscribe(res =>{
        this.mainPageData = this.layout.mainPageData;
      });
    }
    else{
      this.gameService.getMainPage().pipe(
        tap(res => this.mainPageData = res),
        catchError(this.handleError<MainPageDto>('Main page data loading was unsuccessful'))
      ).subscribe(res =>{
        this.mainPageData = this.layout.mainPageData;
      });
    }
  }

  navToBuilding(buildingId: string): void{
    this.router.navigate([`building/${buildingId}`]);
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
