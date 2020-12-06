import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { RefreshDataService } from 'src/app/core/services/refresh-data.service';
import { CountyDto, IUnitDto, MainPageDto, UnitDto, UnitSpecificsDto } from 'src/app/shared/clients';
import { UnitService } from '../services/unit.service';
import { MyUnitDto } from '../models/unit.model';
import { environment } from 'src/environments/environment';
import { MyUnitSpecificsDto } from '../models/unitspecifics.model';
import { SharedService } from 'src/app/core/services/shared-service.service';

@Component({
  selector: 'app-unit-page',
  templateUrl: './unit-page.component.html',
  styleUrls: ['./unit-page.component.scss']
})
export class UnitPageComponent implements OnInit {

  baseUrl = environment.apiUrl;

  countyId: string;

  units: MyUnitDto[];

  unitspecifics: MyUnitSpecificsDto[];
  hireUnitSpecifics: MyUnitSpecificsDto[];

  selectedUnitSpecifics: MyUnitSpecificsDto;

  county: MainPageDto;

  constructor(private service: UnitService,
    private snackbar: MatSnackBar,
    private refreshService: RefreshDataService,
    private route: ActivatedRoute,
    private router: Router,
    private sharedService: SharedService,) { }

  ngOnInit(): void {
    this.countyId = this.route.snapshot.paramMap.get('countyId');

    this.getData();

    this.refreshService.data.pipe(
      tap(res => this.getData()),
      catchError(this.handleError<MyUnitDto[]>('Loading units was unsuccessful', []))
    ).subscribe();
  }

  selectUnitSpecifics(id: string, lvl: number): void{
    this.selectedUnitSpecifics = this.unitspecifics.find(us => us.id.match(id) && us.level == lvl);
    console.log(this.selectedUnitSpecifics);
  }

  setHireCount(amount: number, unitSpecificsId: string){
    this.hireUnitSpecifics.find(u => u.id.match(unitSpecificsId)).desiredCount = amount;
    console.log(amount + '\t' + unitSpecificsId);
  }

  setUpgradeCount(count: number, unitSpecificsId: string){
    this.units.forEach(u => {
      if(u.unitSpecificsId.match(unitSpecificsId))
        u.desiredCount = count;
    })
  }

  upgradeUnits(){
    
    this.units.forEach(u =>{
      this.service.developUnits(this.countyId, u.unitSpecificsId, u.level, u.desiredCount).pipe().subscribe(
        res => {
          this.getData();
          this.sharedService.countyIdUpdated.next(this.countyId);
        })
    });



  }

  hireUnits(){
    this.hireUnitSpecifics.forEach(u => {
      this.service.hireUnits(this.countyId, u.id, u.desiredCount).pipe()
      .subscribe(
        res => {
          this.hireUnitSpecifics.forEach(u => u.desiredCount = 0);
          this.getData();

          this.refreshService.data.pipe(
            tap(res => this.getData()),
            catchError(this.handleError<MyUnitDto[]>('Loading units was unsuccessful', []))
          ).subscribe();

          this.sharedService.countyIdUpdated.next(this.countyId);
        }
      );
    });
  }

  private getData(): void {
    this.service.getUnits(this.countyId).pipe(
      tap(res => this.units = res),
      catchError(this.handleError<MyUnitDto[]>('Loading Units was unsuccessful', []))
    ).subscribe();

    this.service.getCounty(this.countyId).pipe(
      tap(res => this.county = res),
      catchError(this.handleError<MainPageDto>('Loading mainpage was unsuccessful'))
    ).subscribe();

    this.service.getUnitSpecifics().pipe(
      tap(res => this.unitspecifics = res),
      catchError(this.handleError<MyUnitSpecificsDto[]>('Loading unitspecifics was unsuccessful', []))
    ).subscribe(res => {
      this.hireUnitSpecifics = res.filter(f => f.level == 1);
    });


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
