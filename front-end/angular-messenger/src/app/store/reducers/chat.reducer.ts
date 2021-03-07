import { createReducer, on } from '@ngrx/store';

import { Response } from '../../models';
import { Chat } from 'src/app/models/data/chat.model';
import { ChatActions } from '../actions/chat.actions';

const initialState: Chat[] = [];

export const chatReducer = createReducer(
  initialState,
  on(ChatActions.GetChatMessagesSuccess, (state: Chat[], action) => action.chat)
);
