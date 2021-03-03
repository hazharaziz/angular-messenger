import { User } from '../models/data/user.model';

import * as fromAuthReducer from './reducers/auth.reducer';
import { ActionReducerMap, MetaReducer } from '@ngrx/store';
import { environment } from 'src/environments/environment';

export const userStateFeatureKey = 'user';

export type AppState = {
  [userStateFeatureKey]: User;
};

export const reducers: ActionReducerMap<AppState> = {
  [userStateFeatureKey]: fromAuthReducer.authReducer
};

export const metaReducers: MetaReducer<AppState>[] = !environment.production ? [] : [];
