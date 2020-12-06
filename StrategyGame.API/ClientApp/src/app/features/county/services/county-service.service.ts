import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiGameCountiesClient, ApiGameCountyPageClient, ApiGameMainPageClient, ApiGameNewCountyClient, ApiGameNewRoundClient, ApiGameWineConsClient, CountyDto, MainPageDto } from '../../../shared/clients/index';

@Injectable({
  providedIn: 'root'
})
export class CountyServiceService {

  constructor(private newRoundClient: ApiGameNewRoundClient,
            private mainPageClient: ApiGameMainPageClient,
            private countyPageClient: ApiGameCountyPageClient,
            private wineConsumptionClient: ApiGameWineConsClient,
            private newCountyClient: ApiGameNewCountyClient,
            private countiesClient: ApiGameCountiesClient) { }

  newRound(): Observable<void>{
    return this.newRoundClient.newRound();
  }

  getMainPage(): Observable<MainPageDto>{
    return this.mainPageClient.getMainPage();
  }

  getCountyPage(countyId: string): Observable<MainPageDto>{
    return this.countyPageClient.getCountyPage(countyId);
  }

  setWineConsumption(countyId: string, amount: number): Observable<void>{
    return this.wineConsumptionClient.wineCons(countyId, amount);
  }

  newCounty(kingdomId: string, countyName: string): Observable<void>{
    return this.newCountyClient.newCounty(kingdomId, countyName);
  }

  getCounties(kingdomId: string): Observable<CountyDto[]>{
    return this.countiesClient.getCounties(kingdomId);
  }
}
