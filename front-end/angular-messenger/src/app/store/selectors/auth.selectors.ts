import { createFeatureSelector, createSelector } from '@ngrx/store';

import { Authentication } from 'src/app/models/data/authentication.model';
import { authStateFeatureKey } from '..';

export const selectUserState = createFeatureSelector<Authentication>(authStateFeatureKey);

export const AuthSelectors = {
  selectisLoggedIn: createSelector(selectUserState, (state: Authentication) => state?.isLoggedIn),
  selectToken: createSelector(selectUserState, (state: Authentication) => state?.token),
  selectUserId: createSelector(selectUserState, (state: Authentication) => state?.userId)
};
