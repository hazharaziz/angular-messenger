import { createAction, props } from '@ngrx/store';
import { User } from 'src/app/models/data/user.model';
import { ActionTypes } from './types';

export const RelationActions = {
  GetFollowersRequest: createAction(ActionTypes.GetFollowersRequest),
  GetFollowersSucces: createAction(ActionTypes.GetFollowersSuccess, props<{ followers: User[] }>()),
  GetFollowersFail: createAction(ActionTypes.GetFollowersFail, props<{ error: string }>())
};
