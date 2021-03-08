import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { Message } from 'src/app/models/data/message.model';
import { GeneralChatAPI } from 'src/app/models/interfaces/generalChatApi';
import { AppState } from 'src/app/store';
import { AuthSelectors } from 'src/app/store/selectors/auth.selectors';
import { API_URL } from 'src/secrets';

@Injectable({
  providedIn: 'root'
})
export class GeneralChatService implements GeneralChatAPI {
  constructor(private store: Store<AppState>, private http: HttpClient) {}

  getGeneralChatMessages(): Observable<Message[]> {
    return this.http.get<Message[]>(API_URL + '/chat', {
      headers: this.getAuthHeader()
    });
  }

  sendMessageRequest(message: Message): Observable<string> {
    return this.http.post<string>(API_URL + '/chat', message, {
      headers: this.getAuthHeader()
    });
  }

  getAuthHeader(): any {
    let headerToken = '';
    this.store.select(AuthSelectors.selectToken).subscribe((token) => {
      headerToken = `Bearer ${token}`;
    });
    return {
      Authorization: headerToken
    };
  }
}
