import { User } from '../models/data/user.model';

import * as fromAuthReducer from './reducers/auth.reducer';
import * as fromChatReducer from './reducers/chat.reducer';
import { ActionReducerMap, MetaReducer } from '@ngrx/store';
import { environment } from 'src/environments/environment';
import { Chat } from '../models/data/chat.model';

export const userStateFeatureKey = 'user';
export const generalChatStateFeatureKey = 'chat';

export type AppState = {
  [userStateFeatureKey]: User;
  [generalChatStateFeatureKey]: Chat[];
};

export const reducers: ActionReducerMap<AppState> = {
  [userStateFeatureKey]: fromAuthReducer.authReducer,
  [generalChatStateFeatureKey]: fromChatReducer.chatReducer
};

export const metaReducers: MetaReducer<AppState>[] = !environment.production ? [] : [];
