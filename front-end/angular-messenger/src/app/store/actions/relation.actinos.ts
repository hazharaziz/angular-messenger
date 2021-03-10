import { createAction, props } from '@ngrx/store';
import { User } from 'src/app/models/data/user.model';
import { ActionTypes } from './types';

export const RelationActions = {
  GetFollowersRequest: createAction(ActionTypes.GetFollowersRequest),
  GetFollowersSucces: createAction(ActionTypes.GetFollowersSuccess, props<{ followers: User[] }>()),
  GetFollowersFail: createAction(ActionTypes.GetFollowersFail, props<{ error: string }>()),
  RemoveFollowerRequest: createAction(
    ActionTypes.RemoveFollowerRequest,
    props<{ followerId: number }>()
  ),
  RemoveFollowerSuccess: createAction(
    ActionTypes.RemoveFollowerSuccess,
    props<{ followers: User[] }>()
  ),
  RemoveFollowerFail: createAction(ActionTypes.RemoveFollowerFail, props<{ error: string }>())
};
