import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Response } from 'src/app/models';
import { Chat } from 'src/app/models/data/chat.model';
import { GeneralChatAPI } from 'src/app/models/interfaces/generalChatApi';
import { API_URL } from 'src/secrets';

@Injectable({
  providedIn: 'root'
})
export class GeneralChatService implements GeneralChatAPI {
  constructor(private http: HttpClient, token: string) {
    let headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
    http.head(API_URL, {
      headers
    });
  }

  getGeneralChatMessages(): Observable<Response<Chat[]>> {
    return this.http.get('/chat');
  }
}
