import { Component, OnInit } from '@angular/core';
import { BuildingsService } from '../services/buildings.service';
import { tap, catchError } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { of, Observable } from 'rxjs';
import { RefreshDataService } from 'src/app/core/services/refresh-data.service';
import { environment } from 'src/environments/environment';
import { BuildingDetailDto, BuildingNextLevelDto, BuildingStatus } from 'src/app/shared/clients';
import { Route } from '@angular/compiler/src/core';
import { ActivatedRoute } from '@angular/router';
import { SharedService } from 'src/app/core/services/shared-service.service';

@Component({
  selector: 'app-buildings-page',
  templateUrl: './buildings.page.component.html',
  styleUrls: ['./buildings.page.component.scss']
})
export class BuildingsPageComponent implements OnInit {

  baseUrl = environment.apiUrl;
  
  buildingDetail: BuildingDetailDto;
  buildingNextDetail: BuildingNextLevelDto;
  buildingStatusEnum = BuildingStatus;


  constructor(
    private service: BuildingsService,
    private snackbar: MatSnackBar,
    private refreshService: RefreshDataService,
    private route: ActivatedRoute,
    private sharedService: SharedService,) { }

  ngOnInit(): void {
    this.getData();
    this.refreshService.data.subscribe(res => {
      this.getData();
    });
  }

  getData(): void {
    const id = this.route.snapshot.paramMap.get('buildingId');

    this.service.getBuildingDetail(id).pipe(
      tap(res => {
        this.buildingDetail = res;
      }),
      catchError(error => this.handleError<BuildingDetailDto>('Loading building detail was unsuccessful'))
    ).subscribe();

    this.service.getNextLevelDetail(id).pipe(
      tap(res => {
        this.buildingNextDetail = res;
      }),
      catchError(error => this.handleError<BuildingDetailDto>('Loading building detail was unsuccessful'))
    ).subscribe();
  }


  developBuilding(): void {
    this.service.developBuilding(this.buildingDetail.id).pipe(
        tap(res => {
          this.snackbar.open('Successful Upgrade', 'Close', {
            duration: 3000,
            panelClass: ['my-snackbar'],
          });
          this.refreshService.refresh(true);
        }),
        catchError(this.handleError('The upgrade was unsuccessful'))
      ).subscribe(
        res =>{
          this.sharedService.refreshHeader.next(null);
        }
      );
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
