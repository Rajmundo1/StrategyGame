import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiTechnologyDevelopClient, ApiTechnologyTechnologiesClient, ApiTechnologyTechnologyDetailClient, TechnologyDetailDto, TechnologyDto } from '../../../shared/clients/index';

@Injectable({
  providedIn: 'root'
})
export class TechnologyService {

  constructor(private technologiesClient: ApiTechnologyTechnologiesClient,
              private technologyDetailClient: ApiTechnologyTechnologyDetailClient,
              private technologyDevelopClient: ApiTechnologyDevelopClient,) { }

  getTechnologies(kingdomId: string): Observable<TechnologyDto[]>{
    return this.technologiesClient.getTechnologies(kingdomId);
  } 

  getTechnologyDetail(technologyId: string): Observable<TechnologyDetailDto>{
    return this.technologyDetailClient.getTechnologyDetail(technologyId);
  }

  developTechnology(technologyId: string): Observable<TechnologyDto>{
    return this.technologyDevelopClient.developTechnology(technologyId);
  }
}
