import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { catchError, tap } from 'rxjs/operators';
import { RefreshDataService } from 'src/app/core/services/refresh-data.service';
import { SharedService } from 'src/app/core/services/shared-service.service';
import { TechnologyDetailDto, TechnologyDto } from 'src/app/shared/clients';
import { environment } from 'src/environments/environment';
import { TechnologyService } from '../services/technology.service';

@Component({
  selector: 'app-technology-page',
  templateUrl: './technology-page.component.html',
  styleUrls: ['./technology-page.component.scss']
})
export class TechnologyPageComponent implements OnInit {

  baseUrl = environment.apiUrl;

  kingdomId: string;
  technologies: TechnologyDto[];
  selectedTechnology: TechnologyDetailDto;
  selectedTechnologyId: string;

  constructor(private service: TechnologyService,
              private refreshService: RefreshDataService,
              private snackbar: MatSnackBar,
              private route: ActivatedRoute,
              private sharedService: SharedService,) { }

  ngOnInit(): void {
    this.kingdomId = this.route.snapshot.paramMap.get('kingdomId');
    this.getData();

    this.refreshService.data.pipe(
      tap(res => this.getData()),
      catchError(error => this.handleError<TechnologyDto[]>('Loading technologies was unsuccessful'))
    ).subscribe();

  }

  getData(): void{
    this.service.getTechnologies(this.kingdomId).pipe(
      tap(res => {
        this.technologies = res;
      }),
      catchError(error => this.handleError<TechnologyDto[]>('Loading technologies was unsuccessful'))
    ).subscribe();
  }

  selectTechnology(techId: string): void{
    this.service.getTechnologyDetail(techId).pipe(
      tap(res => {
        this.selectedTechnology = res;
        this.selectedTechnologyId = res.id;
      }),
      catchError(error => this.handleError<TechnologyDetailDto>('Loading technology detail was unsuccessful'))
    ).subscribe();
  }

  developTechnology():void{
    this.service.developTechnology(this.selectedTechnology.id).pipe(
      catchError(error => this.handleError<TechnologyDto>('Developing technology was unsuccessful'))
    ).subscribe(res => {
      this.getData();
      this.selectedTechnology = undefined;
      this.selectedTechnologyId = undefined;
      this.sharedService.refreshHeader.next(null);
    });

    console.log(this.technologies);
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
