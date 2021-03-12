import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/store';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  constructor(private store: Store<AppState>) {}

  authorizationHeader(): HttpHeaders {
    let headerToken = '';
    this.store.select(AuthSelectors.selectToken).subscribe((token) => {
      headerToken = `Bearer ${token}`;
    });
    return new HttpHeaders({
      Authorization: headerToken
    });
  }
}
