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
  on(
    DirectActions.GetDirectMessagesSuccess,
    (state: Direct[], payload: { targetId: number; messages: Chat[] }) => {
      let newState: Direct[] = [];
      state.forEach((direct) => newState.push(direct));
      let index = newState.findIndex((direct) => direct.targetId == payload.targetId);
      if (index < 0) return newState;
      newState[index] = { ...newState[index], messages: payload.messages };
      return newState;
    }
  )
);
