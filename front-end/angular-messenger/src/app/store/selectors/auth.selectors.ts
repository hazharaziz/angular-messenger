import { createFeatureSelector, createSelector } from '@ngrx/store';

import { User } from 'src/app/models/data/user.model';
import { userStateFeatureKey } from '..';

export const selectUserState = createFeatureSelector<User>(userStateFeatureKey);

export const AuthSelectors = {
  selectisLoggedIn: createSelector(selectUserState, (state: User) => state?.isLoggedIn),
  selectToken: createSelector(selectUserState, (state: User) => state?.token),
  selectUser: createSelector(selectUserState, (state: User) => state)
};
