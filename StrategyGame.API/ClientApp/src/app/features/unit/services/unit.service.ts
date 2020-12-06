import { DomElementSchemaRegistry } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiGameCountiesClient, ApiGameCountyPageClient, ApiUnitDevelopClient, ApiUnitHireClient, ApiUnitRemoveClient, ApiUnitUnispecificsClient, ApiUnitUnitDetailsClient, ApiUnitUnitNextLevelDetailsClient, ApiUnitUnitsClient, IUnitDto, MainPageDto, UnitDetailsDto, UnitDto, UnitNextLevelDto, UnitSpecificsDto } from '../../../shared/clients';
import { MyUnitDto } from '../models/unit.model';
import { MyUnitSpecificsDto } from '../models/unitspecifics.model'

@Injectable({
  providedIn: 'root'
})
export class UnitService {

  constructor(private unitsClient: ApiUnitUnitsClient,
              private unitDetailsClient: ApiUnitUnitDetailsClient,
              private unitNextLevelDetailClient: ApiUnitUnitNextLevelDetailsClient,
              private unitRemoveClient: ApiUnitRemoveClient,
              private unitDevelopClient: ApiUnitDevelopClient,
              private unitHireClient: ApiUnitHireClient,
              private countyClient: ApiGameCountyPageClient,
              private unitSpecificsClient: ApiUnitUnispecificsClient) { }

  getUnits(countyId: string): Observable<IUnitDto[]>{
    return this.unitsClient.getUnits(countyId).pipe(
      map((dtos: UnitDto[]): MyUnitDto[] =>
               dtos.map(dto => ({
                  unitSpecificsId: dto.unitSpecificsId,
                  count: dto.count,
                  level: dto.level,
                  name: dto.name,
                  maxLevel: dto.maxLevel,
                  imageUrl: dto.imageUrl,
                  desiredCount: 0
              }))
      )
    );
  }

  getUnitSpecifics(): Observable<MyUnitSpecificsDto[]>{
    return this.unitSpecificsClient.getUnitSpecifics().pipe(
      map((dtos: UnitSpecificsDto[]): MyUnitSpecificsDto[] =>
        dtos.map(dto => ({
          id: dto.id,
          name: dto.name,
          imageUrl: dto.imageUrl,
          level: dto.level,
          attackPower: dto.attackPower,
          defensePower: dto.defensePower,
          forceLimit: dto.forceLimit,
          woodCost: dto.woodCost,
          marbleCost: dto.marbleCost,
          wineCost: dto.wineCost,
          sulfurCost: dto.sulfurCost,
          goldCost: dto.goldCost,
          woodUpkeep: dto.woodUpkeep,
          marbleUpkeep: dto.marbleUpkeep,
          wineUpkeep: dto.wineUpkeep,
          sulfurUpkeep: dto.sulfurUpkeep,
          goldUpkeep: dto.goldUpkeep,
          desiredCount: 0
          }))
      )
    );
  }

  getUnitDetails(unitSpecificsId: string, currentLvl: number): Observable<UnitDetailsDto>{
    return this.unitDetailsClient.getUnitDetails(unitSpecificsId, currentLvl);
  }

  getNextLevelDetail(unitSpecificsId: string, currentLvl: number): Observable<UnitNextLevelDto>{
    return this.unitNextLevelDetailClient.getNextLevelDetail(unitSpecificsId, currentLvl);
  }

  removeUnits(countyId: string, unitSpecificsId: string, lvl: number, count: number): Observable<void>{
    return this.unitRemoveClient.removeUnits(countyId, unitSpecificsId, lvl, count);
  }

  developUnits(countyId: string, unitSpecificsId: string, currentLvl: number, count: number): Observable<void>{
    return this.unitDevelopClient.developUnits(countyId, unitSpecificsId, currentLvl, count);
  }

  hireUnits(countyId: string, unitSpecificsId: string, count: number): Observable<void>{
    return this.unitHireClient.hireUnits(countyId, unitSpecificsId, count);
  }

  getCounty(countyId: string): Observable<MainPageDto>{
    return this.countyClient.getCountyPage(countyId);
  }
}
