import { User } from '../models/data/user.model';
import { ActionReducerMap, MetaReducer } from '@ngrx/store';
import { environment } from 'src/environments/environment';
import { Chat } from '../models/data/chat.model';
import { Authentication } from '../models/data/authentication.model';
import { authReducer } from './reducers/auth.reducer';
import { profileReducer } from './reducers/profile.reducer';
import { chatReducer } from './reducers/chat.reducer';
import { Relation } from '../models/data/relation.model';
import { relationReducer } from './reducers/relation.reducer';
import { searchReducer } from './reducers/search.reducer';
import { Group } from '../models/data/group.model';
import { groupReducer } from './reducers/group.reducer';

export const authStateFeatureKey = 'auth';
export const userStateFeatureKey = 'profile';
export const generalChatStateFeatureKey = 'chat';
export const relationStateFeatureKey = 'relations';
export const searchStateFeatureKey = 'searchedUsers';
export const groupStateFeatureKey = 'groups';

export type AppState = {
  [authStateFeatureKey]: Authentication;
  [userStateFeatureKey]: User;
  [generalChatStateFeatureKey]: Chat[];
  [relationStateFeatureKey]: Relation;
  [searchStateFeatureKey]: User[];
  [groupStateFeatureKey]: Group[];
};

export const reducers: ActionReducerMap<AppState> = {
  [authStateFeatureKey]: authReducer,
  [userStateFeatureKey]: profileReducer,
  [generalChatStateFeatureKey]: chatReducer,
  [relationStateFeatureKey]: relationReducer,
  [searchStateFeatureKey]: searchReducer,
  [groupStateFeatureKey]: groupReducer
};

export const metaReducers: MetaReducer<AppState>[] = !environment.production ? [] : [];
