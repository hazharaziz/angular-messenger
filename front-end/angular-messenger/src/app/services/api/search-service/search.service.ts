import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/data/user.model';
import { SearchAPI } from 'src/app/models/interfaces/search.api';
import { API_URL } from 'src/secrets';
import { HttpService } from '../http-service/http.service';

@Injectable({
  providedIn: 'root'
})
export class SearchService implements SearchAPI {
  constructor(private http: HttpClient, private httpService: HttpService) {}

  searchRequest(query: string): Observable<User[]> {
    return this.http.get<User[]>(API_URL + `users/${query}`, {
      headers: this.httpService.authorizationHeader()
    });
  }
}
