import { createReducer, on } from '@ngrx/store';

import { AuthActions } from '../actions/auth.actions';
import { User } from '../../models/data/user.model';
import { Response } from '../../models';

export const authStateFeatureKey = 'auth';

const initialState: User = {
  isLoggedIn: false
};

export const authReducer = createReducer(
  initialState,
  on(AuthActions.LoginSuccess, (state: User, action: Response<User>) => {
    return {
      ...state,
      ...action.data,
      isLoggedIn: true
    };
  }),
  on(AuthActions.LoginFail, (state: User, action: { error: string }) => ({
    ...state,
    error: action.error
  })),
  on(AuthActions.SignUpSuccess, (state: User, action: Response<User>) => ({
    ...state,
    ...action.data,
    isLoggedIn: true
  })),
  on(AuthActions.SignUpFail, (state: User, action: { error: string }) => ({
    ...state,
    error: action.error
  })),
  on(AuthActions.ClearCredentials, (state: User) => undefined)
);
