import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Group } from 'src/app/models/data/group.model';
import { User } from 'src/app/models/data/user.model';
import { GroupAPI } from 'src/app/models/interfaces/group.api';
import { API_URL } from 'src/secrets';
import { HttpService } from '../http-service/http.service';

@Injectable({
  providedIn: 'root'
})
export class GroupService implements GroupAPI {
  constructor(private http: HttpClient, private httpService: HttpService) {}

  getGroupsRequest(): Observable<Group[]> {
    return this.http.get<Group[]>(API_URL + '/groups', {
      headers: this.httpService.authorizationHeader()
    });
  }

  getGroupInfoRequest(groupId: number): Observable<Group> {
    return this.http.get<Group>(API_URL + `/groups/${groupId}`, {
      headers: this.httpService.authorizationHeader()
    });
  }

  getAvailableFriends(groupId: number): Observable<User[]> {
    return this.http.get<User[]>(API_URL + `groups/${groupId}/friends`, {
      headers: this.httpService.authorizationHeader()
    });
  }
}
