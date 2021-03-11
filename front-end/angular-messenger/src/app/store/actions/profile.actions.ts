import { createAction, props } from '@ngrx/store';

import { User } from 'src/app/models/data/user.model';
import { ActionTypes } from './types';

export const ProfileActions = {
  GetProfileRequest: createAction(ActionTypes.GetProfileRequest),
  GetProfileSuccess: createAction(ActionTypes.GetProfileSuccess, props<User>()),
  GetProfileFail: createAction(ActionTypes.GetProfileFail, props<{ error: string }>()),
  EditProfileRequest: createAction(ActionTypes.EditProfileRequest, props<User>()),
  EditProfileSuccess: createAction(ActionTypes.EditProfileSuccess),
  EditProfileFail: createAction(ActionTypes.EditProfileFail, props<{ error: string }>()),
  ChangePasswordRequest: createAction(
    ActionTypes.ChangePasswordRequest,
    props<{ oldPassword: string; newPassword: string }>()
  ),
  ChangePasswordSuccess: createAction(ActionTypes.ChangePasswordSuccess),
  ChangePasswordFail: createAction(ActionTypes.ChangePasswordFail, props<{ error: string }>()),
  DeleteAccountRequest: createAction(ActionTypes.DeleteAccountRequest),
  DeleteAccountSuccess: createAction(ActionTypes.DeleteAccountSuccess),
  DeleteAccountFail: createAction(ActionTypes.DeleteAccountFail, props<{ error: string }>())
};
