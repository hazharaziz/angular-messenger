import { createAction, props } from '@ngrx/store';

import { User } from 'src/app/models/data/user.model';
import { ActionTypes } from './types';

export const ProfileActions = {
  GetProfileRequest: createAction(ActionTypes.GetProfileRequest),
  GetProfileSuccess: createAction(ActionTypes.GetProfileSuccess, props<User>()),
  GetProfileFail: createAction(ActionTypes.GetProfileFail, props<{ error: string }>())
};
