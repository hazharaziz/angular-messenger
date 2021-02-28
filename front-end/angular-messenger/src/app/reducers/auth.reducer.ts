import { createReducer, on } from '@ngrx/store';
import { AppState } from 'src/app/state/app.state';
import { Login } from 'src/app/models/data/login.model';
import { Request } from 'src/app/models/requests/request.model';
import AuthActions from '../actions/auth.actions';
import { User } from '../models/data/user.model';
import { HttpErrorResponse } from '@angular/common/http';
import { Response } from '../models/responses/response.model';

const initialState: AppState = {
  isLoggedIn: false,
  user: {}
};

const _authReducer = createReducer(
  initialState,
  on(
    AuthActions.LoginRequest,
    (state: AppState, action: Request<Login>) => state
  ),
  on(AuthActions.LoginSuccess, (state: AppState, action: Response<User>) => ({
    ...state,
    token: action.token,
    user: action.data
  })),
  on(
    AuthActions.LoginFail,
    (state: AppState, action: { error: HttpErrorResponse }) => ({
      ...state,
      error: action.error
    })
  ),
  on(
    AuthActions.SignUpRequest,
    (state: AppState, action: Request<User>) => state
  ),
  on(AuthActions.SignUpSuccess, (state: AppState, action: Response<User>) => ({
    ...state,
    token: action.token,
    user: action.data
  })),
  on(
    AuthActions.SignUpFail,
    (state: AppState, action: { error: HttpErrorResponse }) => ({
      ...state,
      error: action.error
    })
  )
);

export const authReducer = (state: AppState, action: any) => {
  return _authReducer(state, action);
};
