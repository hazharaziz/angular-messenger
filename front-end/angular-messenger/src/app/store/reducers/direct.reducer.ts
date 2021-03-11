import { createReducer, on } from '@ngrx/store';

import { Chat } from 'src/app/models/data/chat.model';
import { Direct } from 'src/app/models/data/direct.model';
import { DirectActions } from '../actions/direct.actions';

const initialState: Direct[] = [];

export const directReducer = createReducer(
  initialState,
  on(
    DirectActions.GetDirectsSuccess,
    (state: Direct[], payload: { directs: Direct[] }) => payload.directs
  ),
  on(DirectActions.GetDirectMessagesSuccess, (state: Direct[], payload: { messages: Chat[] }) => {
    let newState: Direct[] = [];
    state.forEach((direct) => newState.push(direct));
    if (payload.messages.length == 0) return newState;
    let directId = payload.messages[0].messages[0].directId;
    let index = newState.findIndex((direct) => direct.directId == directId);
    if (index < 0) return newState;
    newState[index] = { ...newState[index], messages: payload.messages };
  })
);
