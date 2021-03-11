import { createReducer, on } from '@ngrx/store';
import { Group } from 'src/app/models/data/group.model';
import { User } from 'src/app/models/data/user.model';
import { GroupActions } from '../actions/group.actions';

const initialState: Group[] = [];

export const groupReducer = createReducer(
  initialState,
  on(
    GroupActions.GetGroupsSuccess,
    (state: Group[], payload: { groups: Group[] }) => payload.groups
  ),
  on(GroupActions.GetGroupInfoSuccess, (state: Group[], payload: { groupInfo: Group }) => {
    let index = state.findIndex((group) => group.groupId == payload.groupInfo.groupId);
    if (index < 0) return state;
    state[index] = { ...state[index], ...payload.groupInfo };
    // let newState: Group[] = [];
    // state.forEach((group) => newState.push(group));
    // let index = newState.findIndex((group) => group.groupId == payload.groupInfo.groupId);
    // if (index < 0) return newState;
    // newState[index] = payload.groupInfo;
    // return newState;
    return state;
  }),
  on(
    GroupActions.GetAvailableFriendsSuccess,
    (state: Group[], payload: { groupId: number; friends: User[] }) => {
      let index = state.findIndex((group) => group.groupId == payload.groupId);
      if (index < 0) return state;
      state[index].friends = payload.friends;
      return state;
    }
  ),
  on(GroupActions.ClearAvailableFriends, (state: Group[], payload: { groupId: number }) => {
    let index = state.findIndex((group) => group.groupId == payload.groupId);
    if (index < 0) return state;
    state[index].friends = [];
    return state;
  })
);
