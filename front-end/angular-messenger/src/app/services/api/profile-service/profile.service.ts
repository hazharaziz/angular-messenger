import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { User } from 'src/app/models/data/user.model';
import { ProfileAPI } from 'src/app/models/interfaces/profile.api';
import { AppState } from 'src/app/store';
import { API_URL } from 'src/secrets';
import { HttpService } from '../http-service/http.service';

@Injectable({
  providedIn: 'root'
})
export class ProfileService implements ProfileAPI {
  constructor(
    private store: Store<AppState>,
    private http: HttpClient,
    private httpService: HttpService
  ) {}

  getProfileRequest(): Observable<User> {
    return this.http.get<User>(API_URL + '/profile', {
      headers: this.httpService.authorizationHeader()
    });
  }

  editProfileRequest(editedUser: User): Observable<string> {
    return this.http.put<string>(API_URL + '/profile', editedUser, {
      headers: this.httpService.authorizationHeader()
    });
  }

  changePasswordRequest(oldPassword: string, newPassword: string): Observable<string> {
    return this.http.put<string>(
      API_URL + '/profile/change-pass',
      {
        oldPassword,
        newPassword
      },
      {
        headers: this.httpService.authorizationHeader()
      }
    );
  }

  deleteAccountRequest(): Observable<string> {
    return this.http.delete<string>(API_URL + '/profile', {
      headers: this.httpService.authorizationHeader()
    });
  }
}
