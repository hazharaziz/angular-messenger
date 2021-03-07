import { Observable } from 'rxjs';

import { Request, Response } from '..';
import { Message } from '../data/message.model';

export interface GeneralChatAPI {
  getGeneralChatMessages(request: Request<null>): Observable<Message[]>;
}
