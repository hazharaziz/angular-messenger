import { createAction, props } from '@ngrx/store';
import { User } from 'src/app/models/data/user.model';
import { ActionTypes } from './types';

export const FriendActions = {
  GetAvailableFriendsRequest: createAction(
    ActionTypes.GetAvailableFriendsRequest,
    props<{ groupId: number }>()
  ),
  GetAvailableFriendsSuccess: createAction(
    ActionTypes.GetAvailableFriendsSuccess,
    props<{ friends: User[] }>()
  ),
  GetAvailableFriendsFail: createAction(
    ActionTypes.GetAvailableFriendsFail,
    props<{ error: string }>()
  ),
  ClearAvailableFriends: createAction(ActionTypes.ClearAvailableFriends)
};
