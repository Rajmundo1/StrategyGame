import { Component, OnInit } from '@angular/core';

import { ScoreboardService } from '../services/scoreboard.service';
import { tap, catchError, distinctUntilChanged, switchMap, debounceTime } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable, of} from 'rxjs';
import { UserDto } from '../../../shared/clients';
import { RefreshDataService } from '../../../core/services/refresh-data.service'

@Component({
  selector: 'app-scoreboard-page',
  templateUrl: './scoreboard.page.component.html',
  styleUrls: ['./scoreboard.page.component.scss']
})
export class ScoreboardPageComponent implements OnInit {

  users: UserDto[];

  constructor(private service: ScoreboardService, private snackbar: MatSnackBar, private refreshService: RefreshDataService) { }

  ngOnInit(): void {
    this.getData();
    this.refreshService.data.subscribe(res => {
      this.getData();
    });
  }

  getData(): void{
    this.service.getUsers().pipe(
      tap(res => this.users = res),
      catchError(error => this.handleError<UserDto[]>('Loading users was unsuccessful', []))
    ).subscribe();
  }

  search(userName: string): void {
    this.service.getFilteredUsers(userName).pipe(
      tap(res => this.users = res),
      catchError(error => this.handleError<UserDto[]>('Loading users was unsuccessful', []))
    ).subscribe();
  }

  private handleError<T>(message = 'Hiba', result?: T) {
    return (error: any): Observable<T> => {
      this.snackbar.open(message, 'Bezár', {
        duration: 3000,
        panelClass: ['my-snackbar'],
      });
      return of(result as T);
    };
  }
}
