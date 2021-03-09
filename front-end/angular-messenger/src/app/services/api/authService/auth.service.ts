import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Request, Response } from '../../../models';
import { User } from 'src/app/models/data/user.model';
import { API_URL } from 'src/secrets';
import { AuthAPI } from '../../../models/interfaces/auth.api';
import { log } from 'src/app/utils/logger';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements AuthAPI {
  constructor(private http: HttpClient) {}

  login(user: Request<User>): Observable<Response<User>> {
    return this.http.post<Response<User>>(API_URL + '/auth/login', user.data);
  }
  signUp(user: Request<User>): Observable<Response<User>> {
    return this.http.post<Response<User>>(API_URL + '/auth/signup', user.data);
  }
}
