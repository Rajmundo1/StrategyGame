import { Component, OnInit, Output, Input } from '@angular/core';

import { AttackService } from '../services/attack.service';
import { tap, catchError, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { of, Observable, Subject } from 'rxjs';
import { RefreshDataService } from 'src/app/core/services/refresh-data.service';
import { AttackDto, AttackUnitDto, CountyDto, UnitDto } from 'src/app/shared/clients';
import { IAttackViewModel } from '../models/attack.model';
import { ActivatedRoute } from '@angular/router';
import { SharedService } from 'src/app/core/services/shared-service.service';

@Component({
  selector: 'app-attack-page',
  templateUrl: './attack.page.component.html',
  styleUrls: ['./attack.page.component.scss']
})
export class AttackPageComponent implements OnInit {

  @Input() unitValue: number;
  @Output() units: number;

  availableUnits: IAttackViewModel[];
  countiesToAttack: CountyDto[];

  outgoingAttacks: AttackDto[];

  attackData: AttackDto = new AttackDto();

  clicked: boolean;

  formatLabel(value: number): number{
    this.units = value;
    return value;
  }

  constructor(
    private service: AttackService,
    private snackbar: MatSnackBar,
    private refreshService: RefreshDataService,
    private route: ActivatedRoute,
    private sharedService: SharedService,
    ) { }

  ngOnInit(): void {

    this.attackData.attackerCountyId = this.route.snapshot.paramMap.get('countyId');

    this.getData();

    this.refreshService.data.pipe(
      tap(res => this.getData()),
      catchError(this.handleError<CountyDto[]>('Loading counties was unsuccessful', []))
    ).subscribe();

  }  

  choose(id: string): void{
    this.attackData.defenderCountyId = id;
    this.clicked = true;
  }

  search(userName: string): void{
    if(userName.length > 0){
      this.service.getAllFilteredCounties(userName).pipe(
        tap(res => this.countiesToAttack = res),
        catchError(this.handleError<CountyDto[]>('Loading counties was unsuccessful', []))
      ).subscribe();
    }
    else{
      this.service.getAllCounties().pipe(
        tap(res => this.countiesToAttack = res),
        catchError(this.handleError<CountyDto[]>('Loading counties was unsuccessful', []))
      ).subscribe();
    }
  }

  sendAttack(): void{
    let units = this.availableUnits.map((model: IAttackViewModel): AttackUnitDto => new AttackUnitDto({
      unitSpecificsId : model.id,
      count: model.sentCount,
      level: model.level
    }));


    this.service.attack(
      this.attackData.attackerCountyId,
      this.attackData.defenderCountyId,
      units
      ).pipe(
        tap(res => {
          this.snackbar.open('Successful attack!', 'Close', {
            duration: 3000,
            panelClass: ['my-snackbar'],
          });
          this.refreshService.refresh(true);
        }),
        catchError(this.handleError('Unsuccessful attack attempt'))
      ).subscribe(
        res=>{
          this.getData();

          this.refreshService.data.pipe(
            tap(res => this.getData()),
            catchError(this.handleError<CountyDto[]>('Loading counties was unsuccessful', []))
          ).subscribe(res => {
            this.attackData.defenderCountyId = undefined;
            this.clicked = false;
            this.sharedService.countyIdUpdated.next(this.attackData.attackerCountyId);
          });
        }
      );
  }

  private getData(): void {
    this.service.getAvailableUnits(this.attackData.attackerCountyId).pipe(
      tap(res => {this.availableUnits = res;}),
      catchError(this.handleError<IAttackViewModel[]>('Loading units was unsuccessful', []))
    ).subscribe();

    this.service.getAllCounties().pipe(
      tap(res => this.countiesToAttack = res),
      catchError(this.handleError<CountyDto[]>('Loading counties was unsuccessful', []))
    ).subscribe();

    this.service.getAttacks(this.attackData.attackerCountyId).pipe(
      tap(res => this.outgoingAttacks = res),
      catchError(this.handleError<AttackDto[]>('Loading attacks was unsuccessful', []))
    ).subscribe(res => console.log(this.outgoingAttacks));
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
