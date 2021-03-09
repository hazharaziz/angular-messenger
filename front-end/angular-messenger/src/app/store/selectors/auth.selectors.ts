import { createFeatureSelector, createSelector } from '@ngrx/store';

import { Authentication } from 'src/app/models/data/authentication.model';
import { authStateFeatureKey } from '..';

const selectAuthState = createFeatureSelector<Authentication>(authStateFeatureKey);

export const AuthSelectors = {
  selectisLoggedIn: createSelector(selectAuthState, (state: Authentication) => state?.isLoggedIn),
  selectToken: createSelector(selectAuthState, (state: Authentication) => state?.token),
  selectUserId: createSelector(selectAuthState, (state: Authentication) => state?.userId)
};
