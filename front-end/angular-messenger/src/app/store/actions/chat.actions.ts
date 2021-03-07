import { createAction, props } from '@ngrx/store';
import { Chat } from 'src/app/models/data/chat.model';

import { Request, Response } from '../../models';
import { ActionTypes } from './types';

export const ChatActions = {
  GetChatMessagesRequest: createAction(ActionTypes.GetChatMessagesRequest, props<Request<null>>()),
  GetChatMessagesSuccess: createAction(
    ActionTypes.GetChatMessagesSuccess,
    props<Response<Chat[]>>()
  ),
  GetChatMessagesFail: createAction(ActionTypes.GetChatMessagesFail, props<{ error: string }>()),
  SendMessageRequest: createAction(ActionTypes.SendMessageRequest, props<Request<null>>()),
  SendMessageSuccess: createAction(ActionTypes.SendMessageSuccess),
  SendMessageFail: createAction(ActionTypes.SendMessageFail, props<{ error: string }>())
};
