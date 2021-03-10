import { createAction, props } from '@ngrx/store';
import { Group } from 'src/app/models/data/group.model';
import { ActionTypes } from './types';

export const GroupActions = {
  GetGroupsRequest: createAction(ActionTypes.GetGroupsRequest),
  GetGroupsSuccess: createAction(ActionTypes.GetGroupsSuccess, props<{ groups: Group[] }>()),
  GetGroupsFail: createAction(ActionTypes.GetGroupsFail, props<{ error: string }>())
};
