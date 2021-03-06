import { Observable } from 'rxjs';

import { Response } from '..';
import { Chat } from '../data/chat.model';

export interface GeneralChatAPI {
  getGeneralChatMessages(): Observable<Response<Chat[]>>;
}
