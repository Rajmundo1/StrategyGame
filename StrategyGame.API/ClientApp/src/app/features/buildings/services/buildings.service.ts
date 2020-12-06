import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiBuildingBuildingDetailClient, ApiBuildingBuildingNextLevelDetailClient, ApiBuildingBuildingsClient, ApiBuildingDevelopClient, BuildingDetailDto, BuildingDto, BuildingNextLevelDto } from '../../../shared/clients/index';

@Injectable({
  providedIn: 'root',
})
export class BuildingsService {

  constructor(private buildingsClient: ApiBuildingBuildingsClient,
              private buildingDetailClient: ApiBuildingBuildingDetailClient,
              private buildingNextLevelDetailClient: ApiBuildingBuildingNextLevelDetailClient,
              private buildingDevelopClient: ApiBuildingDevelopClient) { }

  getBuildings(countyId: string): Observable<BuildingDto[]>{
    return this.buildingsClient.getBuildings(countyId);
  }

  getBuildingDetail(buildingId: string): Observable<BuildingDetailDto>{
    return this.buildingDetailClient.getBuildingDetail(buildingId);
  }

  getNextLevelDetail(buildingId: string): Observable<BuildingNextLevelDto>{
    return this.buildingNextLevelDetailClient.getNextLevelDetail(buildingId);
  }

  developBuilding(buildingId: string): Observable<BuildingDetailDto>{
    return this.buildingDevelopClient.developBuilding(buildingId);
  }

}
