import { Observable } from 'rxjs';

import { Message } from '../data/message.model';

export interface GeneralChatAPI {
  getGeneralChatMessages(): Observable<Message[]>;
  sendMessageRequest(message: Message): Observable<string>;
}
