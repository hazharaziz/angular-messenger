import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { User } from 'src/app/models/data/user.model';
import { ProfileAPI } from 'src/app/models/interfaces/profile.api';
import { AppState } from 'src/app/store';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';
import { API_URL } from 'src/secrets';

@Injectable({
  providedIn: 'root'
})
export class ProfileService implements ProfileAPI {
  constructor(private store: Store<AppState>, private http: HttpClient) {}

  getProfileRequest(): Observable<User> {
    return this.http.get<User>(API_URL + '/profile', {
      headers: this.getAuthHeader()
    });
  }

  editProfileRequest(editedUser: User): Observable<string> {
    return this.http.put<string>(API_URL + '/profile', editedUser, {
      headers: this.getAuthHeader()
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
        headers: this.getAuthHeader()
      }
    );
  }

  private getAuthHeader(): any {
    let headerToken = '';
    this.store.select(AuthSelectors.selectToken).subscribe((token) => {
      headerToken = `Bearer ${token}`;
    });
    return {
      Authorization: headerToken
    };
  }
}
