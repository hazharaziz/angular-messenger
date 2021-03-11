import { createAction, props } from '@ngrx/store';

import { Group } from 'src/app/models/data/group.model';
import { User } from 'src/app/models/data/user.model';
import { ActionTypes } from './types';

export const GroupActions = {
  GetGroupsRequest: createAction(ActionTypes.GetGroupsRequest),
  GetGroupsSuccess: createAction(ActionTypes.GetGroupsSuccess, props<{ groups: Group[] }>()),
  GetGroupsFail: createAction(ActionTypes.GetGroupsFail, props<{ error: string }>()),
  GetGroupInfoRequest: createAction(ActionTypes.GetGroupInfoRequest, props<{ groupId: number }>()),
  GetGroupInfoSuccess: createAction(ActionTypes.GetGroupInfoSuccess, props<{ groupInfo: Group }>()),
  GetGroupInfoFail: createAction(ActionTypes.GetGroupInfoFail, props<{ error: string }>()),
  GetAvailableFriendsRequest: createAction(
    ActionTypes.GetAvailableFriendsRequest,
    props<{ groupId: number }>()
  ),
  GetAvailableFriendsSuccess: createAction(
    ActionTypes.GetAvailableFriendsSuccess,
    props<{ groupId: number; friends: User[] }>()
  ),
  GetAvailableFriendsFail: createAction(
    ActionTypes.GetAvailableFriendsFail,
    props<{ error: string }>()
  ),
  ClearAvailableFriends: createAction(
    ActionTypes.ClearAvailableFriends,
    props<{ groupId: number }>()
  ),
  CreateGroupRequest: createAction(ActionTypes.CreateGroupRequest, props<Group>()),
  CreateGroupSuccess: createAction(ActionTypes.CreateGroupSuccess),
  CreateGroupFail: createAction(ActionTypes.CreateGroupFail, props<{ error: string }>())
};
