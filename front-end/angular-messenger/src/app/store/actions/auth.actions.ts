import { createAction, props } from '@ngrx/store';

import { Request, Response } from '../../models';
import { User } from '../../models/data/user.model';
import { ActionTypes } from './types';

export const AuthActions = {
  LoginRequest: createAction(ActionTypes.LoginRequest, props<Request<User>>()),
  LoginSuccess: createAction(ActionTypes.LoginSuccess, props<Response<User>>()),
  LoginFail: createAction(ActionTypes.LoginFail, props<{ error: string }>()),
  SignUpRequest: createAction(ActionTypes.SignUpRequest, props<Request<User>>()),
  SignUpSuccess: createAction(ActionTypes.SignUpSuccess, props<Response<User>>()),
  SignUpFail: createAction(ActionTypes.SignUpFail, props<{ error: string }>()),
  ClearCredentials: createAction(ActionTypes.ClearCredentials)
};
