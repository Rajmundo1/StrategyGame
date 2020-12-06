import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import {AttackDto, ApiAttackAttacksClient, ApiAttackAttackClient, AttackUnitDto, ApiGameCountiesClient, ApiGameAllCountiesClient, CountyDto, ApiGameAllCountiesFilteredClient, IAttackUnitDto, ApiUnitUnitsClient, UnitDto} from '../../../shared/clients/index';
import { IAttackViewModel } from '../models/attack.model';

@Injectable({
  providedIn: 'root',
})
export class AttackService {

  constructor(private attacksClient: ApiAttackAttacksClient,
             private attackClient: ApiAttackAttackClient,
             private allCountiesClient: ApiGameAllCountiesClient,
             private allFilteredCountiesClient: ApiGameAllCountiesFilteredClient,
             private unitClient: ApiUnitUnitsClient) { }

  getAttacks(countyId: string): Observable<AttackDto[]>{
    return this.attacksClient.getAttacks(countyId);
  }

  attack(attackerCountyId: string, defenderCountyId: string, units: AttackUnitDto[]): Observable<void>{
    return this.attackClient.attack(attackerCountyId, defenderCountyId, units);
  }

  getAllCounties(): Observable<CountyDto[]>{
    return this.allCountiesClient.getAllCountiesAll();
  }

  getAllFilteredCounties(userName: string): Observable<CountyDto[]>{
    return this.allFilteredCountiesClient.getAllCounties(userName);
  }

  getAvailableUnits(countyId: string): Observable<IAttackViewModel[]>{
    return this.unitClient.getUnits(countyId).pipe(
      map((dtos: UnitDto[]): IAttackViewModel[] =>
               dtos.map(dto => ({
                  id: dto.unitSpecificsId,
                  level: dto.level,
                  name: dto.name,
                  availableCount: dto.count,
                  imageUrl: dto.imageUrl,
                  sentCount: 0
              }))
      )
    );
  }
}
