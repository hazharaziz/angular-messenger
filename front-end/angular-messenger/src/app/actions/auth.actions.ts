import { HttpErrorResponse } from '@angular/common/http';
import { createAction, props } from '@ngrx/store';
import { Login } from 'src/app/models/data/login.model';
import { Request } from 'src/app/models/requests/request.model';
import { User } from '../models/data/user.model';
import { Response } from '../models/responses/response.model';
import ActionTypes from './types';

const AuthActions = {
  LoginRequest: createAction(ActionTypes.LoginRequest, props<Request<Login>>()),
  LoginSuccess: createAction(ActionTypes.LoginSuccess, props<Response<User>>()),
  LoginFail: createAction(
    ActionTypes.LoginFail,
    props<{ error: HttpErrorResponse }>()
  ),
  SignUpRequest: createAction(
    ActionTypes.SignUpRequest,
    props<Request<User>>()
  ),
  SignUpSuccess: createAction(
    ActionTypes.SignUpSuccess,
    props<Response<User>>()
  ),
  SignUpFail: createAction(
    ActionTypes.SignUpFail,
    props<{ error: HttpErrorResponse }>()
  )
};

export default AuthActions;
