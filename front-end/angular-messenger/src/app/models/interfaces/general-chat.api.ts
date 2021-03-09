import { Observable } from 'rxjs';

import { Message } from '../data/message.model';

export interface GeneralChatAPI {
  getGeneralChatMessages(): Observable<Message[]>;
  sendMessageRequest(message: Message): Observable<string>;
  editMessageRequest(messageId: number, message: Message): Observable<string>;
  deleteMessageRequest(messageId: number): Observable<string>;
}
