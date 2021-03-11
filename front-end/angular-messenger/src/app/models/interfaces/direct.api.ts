import { Observable } from 'rxjs';

import { Direct } from '../data/direct.model';
import { Message } from '../data/message.model';

export interface DirectAPI {
  getDirectsRequest(): Observable<Direct[]>;
  getDirectMessagesRequest(targetId: number): Observable<Message[]>;
  sendDirectMessageRequest(targetId: number, message: Message): Observable<string>;
  editDirectMessageRequest(
    targetId: number,
    messageId: number,
    message: Message
  ): Observable<string>;
  deleteDirectMessageRequest(targetId: number, messageId: number): Observable<string>;
  deleteDirectChatHistoryRequest(targetId: number): Observable<string>;
  deleteDirectRequest(directId: number): Observable<string>;
}
