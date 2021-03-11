import { createAction, props } from '@ngrx/store';
import { Chat } from 'src/app/models/data/chat.model';

import { Group } from 'src/app/models/data/group.model';
import { Message } from 'src/app/models/data/message.model';
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
  CreateGroupFail: createAction(ActionTypes.CreateGroupFail, props<{ error: string }>()),
  EditGroupRequest: createAction(ActionTypes.EditGroupRequest, props<Group>()),
  EditGroupFail: createAction(ActionTypes.EditGroupFail, props<{ error: string }>()),
  DeleteGroupRequest: createAction(ActionTypes.DeleteGroupRequest, props<{ groupId: number }>()),
  DeleteGroupFail: createAction(ActionTypes.DeleteGroupFail, props<{ error: string }>()),
  AddMemberToGroupRequest: createAction(
    ActionTypes.AddMemberToGroupRequest,
    props<{ groupId: number; members: number[] }>()
  ),
  AddMemberToGroupFail: createAction(ActionTypes.AddMemberToGroupFail, props<{ error: string }>()),
  RemoveMemberFromGroupRequest: createAction(
    ActionTypes.RemoveMemberFromGroupRequest,
    props<{ groupId: number; memberId: number }>()
  ),
  RemoveMemberFromGroupFail: createAction(
    ActionTypes.RemoveMemberFromGroupFail,
    props<{ error: string }>()
  ),
  GetGroupMessagesRequest: createAction(
    ActionTypes.GetGroupMessagesRequest,
    props<{ groupId: number }>()
  ),
  GetGroupMessagesSuccess: createAction(
    ActionTypes.GetGroupMessagesSuccess,
    props<{ groupId: number; messages: Chat[] }>()
  ),
  GetGroupMessagesFail: createAction(ActionTypes.GetGroupMessagesFail, props<{ error: string }>()),
  SendGroupMessageRequest: createAction(ActionTypes.SendGroupMessageRequest, props<Message>()),
  SendGroupMessageFail: createAction(ActionTypes.SendGroupMessagesFail, props<{ error: string }>()),
  EditGroupMessageRequest: createAction(
    ActionTypes.EditGroupMessagesRequest,
    props<{ groupId: number; messageId: number; message: string }>()
  ),
  EditGroupMessageFail: createAction(ActionTypes.EditGroupMessageFail, props<{ error: string }>()),
  DeleteGroupMessageRequest: createAction(
    ActionTypes.DeleteGroupMessagesRequest,
    props<{ groupId: number; messageId: number }>()
  ),
  DeleteGroupMessageFail: createAction(
    ActionTypes.DeleteGroupMessageFail,
    props<{ error: string }>()
  ),
  ClearGroupChatHistoryRequest: createAction(
    ActionTypes.ClearGroupChatHistoryRequest,
    props<{ groupId: number }>()
  ),
  ClearGroupChatHistoryFail: createAction(
    ActionTypes.ClearGroupChatHistoryFail,
    props<{ error: string }>()
  ),
  LeaveGroupRequest: createAction(ActionTypes.LeaveGroupRequest, props<{ groupId: number }>()),
  LeaveGroupFail: createAction(ActionTypes.LeaveGroupFail, props<{ error: string }>())
};
