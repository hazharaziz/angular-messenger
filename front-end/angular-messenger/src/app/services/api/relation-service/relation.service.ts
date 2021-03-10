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

  removeFollowerRequest(followerId: number): Observable<string> {
    return this.http.delete<string>(API_URL + `/relations/delete-follower/${followerId}`, {
      headers: this.httpService.authorizationHeader()
    });
  }

  getFollowingsRequest(): Observable<User[]> {
    return this.http.get<User[]>(API_URL + '/relations/followings', {
      headers: this.httpService.authorizationHeader()
    });
  }

  unfollowRequest(followingId: number): Observable<string> {
    return this.http.delete<string>(API_URL + `/relations/unfollow/${followingId}`, {
      headers: this.httpService.authorizationHeader()
    });
  }

  getRequestsSentRequest(): Observable<User[]> {
    return this.http.get<User[]>(API_URL + '/relations/requests/sent', {
      headers: this.httpService.authorizationHeader()
    });
  }

  cancelRequestRequest(userId: number): Observable<string> {
    return this.http.delete<string>(API_URL + `/relations/cancel-request/${userId}`, {
      headers: this.httpService.authorizationHeader()
    });
  }

  getRequestsReceivedRequest(): Observable<User[]> {
    return this.http.get<User[]>(API_URL + '/relations/requests/received', {
      headers: this.httpService.authorizationHeader()
    });
  }

  acceptRequestRequest(userId: number): Observable<string> {
    return this.http.put<string>(API_URL + `/relations/accept-request/${userId}`, {
      headers: this.httpService.authorizationHeader()
    });
  }
}
