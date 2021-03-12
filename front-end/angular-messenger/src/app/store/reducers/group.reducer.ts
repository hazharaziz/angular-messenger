import { createReducer, on } from '@ngrx/store';
import { Chat } from 'src/app/models/data/chat.model';
import { Group } from 'src/app/models/data/group.model';
import { Message } from 'src/app/models/data/message.model';
import { User } from 'src/app/models/data/user.model';
import { log } from 'src/app/utils/logger';
import { GroupActions } from '../actions/group.actions';

const initialState: Group[] = [];

export const groupReducer = createReducer(
  initialState,
  on(
    GroupActions.GetGroupsSuccess,
    (state: Group[], payload: { groups: Group[] }) => payload.groups
  ),
  on(GroupActions.GetGroupInfoSuccess, (state: Group[], payload: { groupInfo: Group }) => {
    let newState: Group[] = [];
    state.forEach((group) => newState.push(group));
    let index = newState.findIndex((group) => group.groupId == payload.groupInfo.groupId);
    if (index < 0) return newState;
    newState[index] = payload.groupInfo;
    return newState;
  }),
  on(
    GroupActions.GetAvailableFriendsSuccess,
    (state: Group[], payload: { groupId: number; friends: User[] }) => {
      let newState: Group[] = [];
      state.forEach((group) => newState.push(group));
      let index = newState.findIndex((group) => group.groupId == payload.groupId);
      if (index < 0) return newState;
      newState[index] = { ...newState[index], friends: payload.friends };
      return newState;
    }
  ),
  on(GroupActions.ClearAvailableFriends, (state: Group[], payload: { groupId: number }) => {
    let newState: Group[] = [];
    state.forEach((group) => newState.push(group));
    let index = newState.findIndex((group) => group.groupId == payload.groupId);
    if (index < 0) return newState;
    newState[index].friends = [];
    return newState;
  }),
  on(
    GroupActions.GetGroupMessagesSuccess,
    (state: Group[], payload: { groupId: number; messages: Chat[] }) => {
      let newState: Group[] = [];
      state.forEach((group) => newState.push(group));
      let index = newState.findIndex((group) => group.groupId == payload.groupId);
      if (index < 0) return newState;
      newState[index] = { ...newState[index], messages: payload.messages };
      return newState;
    }
  )
);
