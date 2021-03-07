import { createReducer, on } from '@ngrx/store';

import { AuthActions } from '../actions/auth.actions';
import { User } from '../../models/data/user.model';
import { Response } from '../../models';

const initialState: User = {
  isLoggedIn: false
};

export const authReducer = createReducer(
  initialState,
  on(AuthActions.LoginSuccess, (state: User, action: Response<User>) => ({
    ...action.data,
    isLoggedIn: true,
    token: action.token
  })),
  on(AuthActions.SignUpSuccess, (state: User, action: Response<User>) => ({
    ...action.data,
    isLoggedIn: true,
    token: action.token
  })),
  on(AuthActions.ClearCredentials, (state: User) => undefined)
);
