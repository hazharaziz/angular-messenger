import { createAction, props } from '@ngrx/store';

import { Chat } from 'src/app/models/data/chat.model';
import { Direct } from 'src/app/models/data/direct.model';
import { Message } from 'src/app/models/data/message.model';
import { ActionTypes } from './types';

export const DirectActions = {
  GetDirectsRequest: createAction(ActionTypes.GetDirectsRequest),
  GetDirectsSuccess: createAction(ActionTypes.GetDirectsSuccess, props<{ directs: Direct[] }>()),
  GetDirectsFail: createAction(ActionTypes.GetDirectsFail, props<{ error: string }>()),
  GetDirectMessagesRequest: createAction(
    ActionTypes.GetDirectMessagesRequest,
    props<{ targetId: number }>()
  ),
  GetDirectMessagesSuccess: createAction(
    ActionTypes.GetDirectMessagesSuccess,
    props<{ messages: Chat[] }>()
  ),
  GetDirectMessagesFail: createAction(
    ActionTypes.GetDirectMessagesFail,
    props<{ error: string }>()
  ),
  SendDirectMessageRequest: createAction(
    ActionTypes.SendDirectMessageRequest,
    props<{ directMessage: Message }>()
  ),
  SendDirectMessageFail: createAction(
    ActionTypes.SendDirectMessageFail,
    props<{ error: string }>()
  ),
  EditDirectMessageRequest: createAction(
    ActionTypes.EditDirectMessageRequest,
    props<{ directMessage: Message }>()
  ),
  EditDirectMessageFail: createAction(
    ActionTypes.EditDirectMessageFail,
    props<{ error: string }>()
  ),
  DeleteDirectMessageRequest: createAction(
    ActionTypes.DeleteDirectMessageRequest,
    props<{ directId: number; messageId: number }>()
  ),
  DeleteDirectMessageFail: createAction(
    ActionTypes.DeleteDirectMessageFail,
    props<{ error: string }>()
  ),
  ClearDirectChatHistoryRequest: createAction(
    ActionTypes.ClearDirectChatHistoryRequest,
    props<{ targetId: number }>()
  ),
  ClearDirectChatHistoryFail: createAction(
    ActionTypes.ClearDirectChatHistoryFail,
    props<{ error: string }>()
  ),
  RemoveDirectRequest: createAction(ActionTypes.RemoveDirectRequest, props<{ directId: number }>()),
  RemoveDirectFail: createAction(ActionTypes.RemoveDirectFail, props<{ error: string }>())
};
