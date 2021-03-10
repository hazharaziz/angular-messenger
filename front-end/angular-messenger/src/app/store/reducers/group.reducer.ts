import { createReducer, on } from '@ngrx/store';
import { Group } from 'src/app/models/data/group.model';
import { GroupActions } from '../actions/group.actions';

const initialState: Group[] = [];

export const groupReducer = createReducer(
  initialState,
  on(
    GroupActions.GetGroupsSuccess,
    (state: Group[], payload: { groups: Group[] }) => payload.groups
  )
);
