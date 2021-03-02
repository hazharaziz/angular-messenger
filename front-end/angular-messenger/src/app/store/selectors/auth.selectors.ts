import { createFeatureSelector, createSelector } from '@ngrx/store';

import { User } from 'src/app/models/data/user.model';
import { authStateFeatureKey } from '../reducers/auth.reducer';

export const selectUserState = createFeatureSelector<User>(authStateFeatureKey);

export const AuthSelectors = {
  selectisLoggedIn: createSelector(selectUserState, (state: User) => state?.isLoggedIn),
  selectToken: createSelector(selectUserState, (state: User) => state?.token),
  selectUser: createSelector(selectUserState, (state: User) => state)
};
