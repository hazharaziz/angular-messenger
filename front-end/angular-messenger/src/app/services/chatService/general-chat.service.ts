import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Request } from 'src/app/models';
import { Message } from 'src/app/models/data/message.model';
import { GeneralChatAPI } from 'src/app/models/interfaces/generalChatApi';
import { API_URL } from 'src/secrets';

@Injectable({
  providedIn: 'root'
})
export class GeneralChatService implements GeneralChatAPI {
  constructor(private http: HttpClient) {}

  getGeneralChatMessages(request: Request<null>): Observable<Message[]> {
    return this.http.get<Message[]>(API_URL + '/chat', {
      headers: { Authorization: `Bearer ${request.token}` }
    });
  }
}
