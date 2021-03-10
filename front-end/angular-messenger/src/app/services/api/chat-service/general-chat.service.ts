import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { Message } from 'src/app/models/data/message.model';
import { GeneralChatAPI } from 'src/app/models/interfaces/general-chat.api';
import { AppState } from 'src/app/store';
import { API_URL } from 'src/secrets';
import { HttpService } from '../http-service/http.service';

@Injectable({
  providedIn: 'root'
})
export class GeneralChatService implements GeneralChatAPI {
  constructor(
    private store: Store<AppState>,
    private http: HttpClient,
    private httpService: HttpService
  ) {}

  getGeneralChatMessages(): Observable<Message[]> {
    return this.http.get<Message[]>(API_URL + '/chat', {
      headers: this.httpService.authorizationHeader()
    });
  }

  sendMessageRequest(message: Message): Observable<string> {
    return this.http.post<string>(API_URL + '/chat', message, {
      headers: this.httpService.authorizationHeader()
    });
  }

  editMessageRequest(messageId: number, message: Message): Observable<string> {
    return this.http.put<string>(API_URL + `/chat/${messageId}`, message, {
      headers: this.httpService.authorizationHeader()
    });
  }

  deleteMessageRequest(messageId: number): Observable<string> {
    return this.http.delete<string>(API_URL + `/chat/${messageId}`, {
      headers: this.httpService.authorizationHeader()
    });
  }
}
