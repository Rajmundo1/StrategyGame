import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  countyIdUpdated: BehaviorSubject<string> = new BehaviorSubject('');
  refreshHeader: BehaviorSubject<Object> = new BehaviorSubject(null);

  constructor() { }
}
