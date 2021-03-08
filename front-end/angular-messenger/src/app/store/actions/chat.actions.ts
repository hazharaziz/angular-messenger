import { createAction, props } from '@ngrx/store';

import { Chat } from 'src/app/models/data/chat.model';
import { Message } from 'src/app/models/data/message.model';
import { Response } from '../../models';
import { ActionTypes } from './types';

export const ChatActions = {
  GetChatMessagesRequest: createAction(ActionTypes.GetChatMessagesRequest),
  GetChatMessagesSuccess: createAction(
    ActionTypes.GetChatMessagesSuccess,
    props<Response<Chat[]>>()
  ),
  GetChatMessagesFail: createAction(ActionTypes.GetChatMessagesFail, props<{ error: string }>()),
  SendMessageRequest: createAction(ActionTypes.SendMessageRequest, props<Message>()),
  SendMessageFail: createAction(ActionTypes.SendMessageFail, props<{ error: string }>()),
  EditMessageRequest: createAction(ActionTypes.EditMessageRequest, props<Message>()),
  EditMessageFail: createAction(ActionTypes.EditMessageFail, props<{ error: string }>())
};
