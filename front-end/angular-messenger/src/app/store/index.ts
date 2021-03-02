import { User } from '../models/data/user.model';

import * as fromAuthReducer from './reducers/auth.reducer';
import { ActionReducerMap, MetaReducer } from '@ngrx/store';
import { environment } from 'src/environments/environment';

export type AppState = {
  [fromAuthReducer.authStateFeatureKey]: User;
};

export const reducers: ActionReducerMap<AppState> = {
  [fromAuthReducer.authStateFeatureKey]: fromAuthReducer.authReducer
};

export const metaReducers: MetaReducer<AppState>[] = !environment.production ? [] : [];
