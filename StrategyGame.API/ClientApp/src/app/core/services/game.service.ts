import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiGameCountyPageClient, ApiGameMainPageClient, ApiGameNewRoundClient, MainPageDto } from 'src/app/shared/clients';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private mainPageClient: ApiGameMainPageClient,
              private newRoundClient: ApiGameNewRoundClient,
              private countyPageClient: ApiGameCountyPageClient) { }

  getMainPage(): Observable<MainPageDto>{
    return this.mainPageClient.getMainPage();
  }

  getCountyPage(countyId: string): Observable<MainPageDto>{
    return this.countyPageClient.getCountyPage(countyId);
  }

  newRound(): Observable<void>{
    return this.newRoundClient.newRound();
  }
}
