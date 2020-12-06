import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';

@Injectable({
    providedIn: 'root',
})
export class RefreshDataService {

    private dataSource = new BehaviorSubject<boolean>(false);
    data = this.dataSource.asObservable();

    constructor() { }

    refresh(data: boolean): void{
        this.dataSource.next(data);
    }

}
