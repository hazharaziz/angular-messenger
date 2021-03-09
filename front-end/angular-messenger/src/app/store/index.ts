import { User } from '../models/data/user.model';
import { ActionReducerMap, MetaReducer } from '@ngrx/store';
import { environment } from 'src/environments/environment';
import { Chat } from '../models/data/chat.model';
import { Authentication } from '../models/data/authentication.model';
import { authReducer } from './reducers/auth.reducer';
import { profileReducer } from './reducers/profile.reducer';
import { chatReducer } from './reducers/chat.reducer';

export const authStateFeatureKey = 'auth';
export const userStateFeatureKey = 'profile';
export const generalChatStateFeatureKey = 'chat';

export type AppState = {
  [authStateFeatureKey]: Authentication;
  [userStateFeatureKey]: User;
  [generalChatStateFeatureKey]: Chat[];
};

export const reducers: ActionReducerMap<AppState> = {
  [authStateFeatureKey]: authReducer,
  [userStateFeatureKey]: profileReducer,
  [generalChatStateFeatureKey]: chatReducer
};

export const metaReducers: MetaReducer<AppState>[] = !environment.production ? [] : [];
