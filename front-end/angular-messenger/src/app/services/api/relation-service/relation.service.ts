import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { User } from 'src/app/models/data/user.model';
import { RelationAPI } from 'src/app/models/interfaces/relation.api';
import { API_URL } from 'src/secrets';
import { HttpService } from '../http-service/http.service';

@Injectable({
  providedIn: 'root'
})
export class RelationService implements RelationAPI {
  constructor(private http: HttpClient, private httpService: HttpService) {}

  getFollowersRequest(): Observable<User[]> {
    return this.http.get<User[]>(API_URL + '/relations/followers', {
      headers: this.httpService.authorizationHeader()
    });
  }
}
