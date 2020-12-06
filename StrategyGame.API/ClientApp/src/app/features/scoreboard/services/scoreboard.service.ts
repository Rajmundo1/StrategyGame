import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiUserFilteredUsersClient, ApiUserUsersClient, UserDto } from '../../../shared/clients';


@Injectable({
    providedIn: 'root',
  })
  export class ScoreboardService {

    constructor(private usersClient: ApiUserUsersClient,
                private filteredUserClient: ApiUserFilteredUsersClient) { }

    getFilteredUsers(userName: string): Observable<UserDto[]>{
        return this.filteredUserClient.getFilteredUsers(userName);
    }

    getUsers(): Observable<UserDto[]>{
      return this.usersClient.getUsers();
    }
  }
