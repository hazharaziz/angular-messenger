import {
  HttpClient,
  HttpErrorResponse,
  HttpResponse
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Login } from 'src/app/models/data/login.model';
import { User } from 'src/app/models/data/user.model';
import { Request } from 'src/app/models/requests/request.model';
import AuthActions from 'src/app/actions/auth.actions';
import { API_URL } from 'src/secrets';
import { AuthAPI } from '../interfaces/authApi';
import { Response } from 'src/app/models/responses/response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements AuthAPI {
  constructor(private http: HttpClient) {}

  login(user: Request<Login>): Observable<Response<User>> {
    return this.http.post<Response<User>>(API_URL + '/auth/login', user.data);
  }
  signUp(user: Request<User>): Observable<Response<User>> {
    return this.http.post<Response<User>>(API_URL + '/auth/signup', user.data);
  }
}
