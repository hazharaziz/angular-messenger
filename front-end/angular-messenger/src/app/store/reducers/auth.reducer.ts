import { createReducer, on } from '@ngrx/store';

import { AuthActions } from '../actions/auth.actions';
import { Response } from '../../models';
import { Authentication } from 'src/app/models/data/authentication.model';
import { User } from 'src/app/models/data/user.model';

const initialState: Authentication = {
  isLoggedIn: false
};

export const authReducer = createReducer(
  initialState,
  on(AuthActions.LoginSuccess, (state: Authentication, action: Response<User>) => ({
    isLoggedIn: true,
    token: action.token,
    userId: action.data.id
  })),
  on(AuthActions.SignUpSuccess, (state: Authentication, action: Response<User>) => ({
    isLoggedIn: true,
    token: action.token,
    userId: action.data.id
  })),
  on(AuthActions.ClearCredentials, (state: Authentication) => ({}))
);
