import { createAction, props } from '@ngrx/store';
import { Chat } from 'src/app/models/data/chat.model';

import { Request, Response } from '../../models';
import { ActionTypes } from './types';

export const ChatActions = {
  GetChatMessagesRequest: createAction(ActionTypes.GetChatMessagesRequest, props<Request<null>>()),
  GetChatMessagesSuccess: createAction(
    ActionTypes.GetChatMessagesSuccess,
    props<{ chat: Chat[] }>()
  ),
  GetChatMessagesFail: createAction(ActionTypes.GetChatMessagesFail, props<{ error: string }>())
};
