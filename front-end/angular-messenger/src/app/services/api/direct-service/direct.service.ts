import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Direct } from 'src/app/models/data/direct.model';
import { Message } from 'src/app/models/data/message.model';
import { DirectAPI } from 'src/app/models/interfaces/direct.api';
import { API_URL } from 'src/secrets';
import { HttpService } from '../http-service/http.service';

@Injectable({
  providedIn: 'root'
})
export class DirectService implements DirectAPI {
  constructor(private http: HttpClient, private httpService: HttpService) {}

  getDirectsRequest(): Observable<Direct[]> {
    return this.http.get<Direct[]>(API_URL + '/directs', {
      headers: this.httpService.authorizationHeader()
    });
  }

  getDirectMessagesRequest(targetId: number): Observable<Message[]> {
    return this.http.get<Message[]>(API_URL + `/directs/messages/${targetId}`, {
      headers: this.httpService.authorizationHeader()
    });
  }

  sendDirectMessageRequest(targetId: number, message: Message): Observable<string> {
    return this.http.post<string>(API_URL + `/directs/message/${targetId}`, message, {
      headers: this.httpService.authorizationHeader()
    });
  }

  editDirectMessageRequest(
    targetId: number,
    messageId: number,
    message: Message
  ): Observable<string> {
    return this.http.put<string>(API_URL + `/directs/message/${targetId}/${messageId}`, message, {
      headers: this.httpService.authorizationHeader()
    });
  }

  deleteDirectMessageRequest(targetId: number, messageId: number): Observable<string> {
    return this.http.delete<string>(API_URL + `/directs/message/${targetId}/${messageId}`, {
      headers: this.httpService.authorizationHeader()
    });
  }

  deleteDirectChatHistoryRequest(targetId: number): Observable<string> {
    return this.http.delete<string>(API_URL + `/directs/history/${targetId}`, {
      headers: this.httpService.authorizationHeader()
    });
  }

  deleteDirectRequest(directId: number): Observable<string> {
    return this.http.delete<string>(API_URL + `/directs/${directId}`, {
      headers: this.httpService.authorizationHeader()
    });
  }
}
