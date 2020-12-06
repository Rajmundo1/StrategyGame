import { Component, OnInit, Output, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, tap } from 'rxjs/operators';
import { RefreshDataService } from 'src/app/core/services/refresh-data.service';
import { CountyDto } from 'src/app/shared/clients';
import { MatSlider } from '@angular/material/slider';
import { ActivatedRoute, Router } from '@angular/router';
import { CountyServiceService } from '../services/county-service.service';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-county-page',
  templateUrl: './county-page.component.html',
  styleUrls: ['./county-page.component.scss']
})
export class CountyPageComponent implements OnInit {

  kingdomId: string;
  counties: CountyDto[];

  choosedCountyId: string;
  choosedCounty: CountyDto;
  choosedWineCons: number;

  newCountyName: string;

  constructor(private service: CountyServiceService,
    private snackbar: MatSnackBar,
    private refreshService: RefreshDataService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.kingdomId = this.route.snapshot.paramMap.get('kingdomId');

    this.getData();

    this.refreshService.data.pipe(
      tap(res => this.getData()),
      catchError(this.handleError<CountyDto[]>('Loading counties was unsuccessful', []))
    ).subscribe();

  }

  createNewCounty(): void{
    this.service.newCounty(this.kingdomId, this.newCountyName).pipe().subscribe(
      res => {
        this.getData();

        this.refreshService.data.pipe(
          tap(res => this.getData()),
          catchError(this.handleError<CountyDto[]>('Loading counties was unsuccessful', []))
        ).subscribe();
      }
    );
  }

  setCountyName(countyName: string): void{
    this.newCountyName = countyName;
  }

  setWineCons(): void{
    this.service.setWineConsumption(this.choosedCountyId, this.choosedWineCons).pipe().subscribe();
  }

  changeWineCons(wineCons: number): void{
    this.choosedWineCons = wineCons;
  }

  navigate(): void{
    this.router.navigate([`mainPage/${this.choosedCountyId}`]);
  }

  choose(countyId: string): void{
    this.choosedCountyId = countyId;
    this.choosedCounty = this.counties.find(c => c.id.match(this.choosedCountyId));
  }

  private getData(): void {
    this.service.getCounties(this.kingdomId).pipe(
      tap(res => this.counties = res),
      catchError(this.handleError<CountyDto[]>('Loading Counties was unsuccessful', []))
    ).subscribe();
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
