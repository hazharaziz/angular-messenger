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
  RemoveFollowerFail: createAction(ActionTypes.RemoveFollowerFail, props<{ error: string }>()),
  GetFollowingsRequest: createAction(ActionTypes.GetFollowingsRequest),
  GetFollowingsSucces: createAction(
    ActionTypes.GetFollowingsSuccess,
    props<{ followings: User[] }>()
  ),
  GetFollowingsFail: createAction(ActionTypes.GetFollowingsFail, props<{ error: string }>()),
  UnfollowRequest: createAction(ActionTypes.UnfollowRequest, props<{ followingId: number }>()),
  UnfollowFail: createAction(ActionTypes.UnfollowFail, props<{ error: string }>()),
  GetRequestsSentRequest: createAction(ActionTypes.GetRequestsSentRequest),
  GetRequestsSentSucces: createAction(
    ActionTypes.GetRequestsSentSuccess,
    props<{ sentRequests: User[] }>()
  ),
  GetRequestsSentFail: createAction(ActionTypes.GetRequestsSentFail, props<{ error: string }>()),
  CancelRequestRequest: createAction(ActionTypes.CancelRequestRequest, props<{ userId: number }>()),
  CancelRequestFail: createAction(ActionTypes.CancelRequestFail, props<{ error: string }>()),
  AcceptRequestRequest: createAction(ActionTypes.AcceptRequestRequest, props<{ userId: number }>()),
  AcceptRequestFail: createAction(ActionTypes.AcceptRequestFail, props<{ error: string }>()),
  GetRequestsReceivedRequest: createAction(ActionTypes.GetRequestsReceivedRequest),
  GetRequestsReceivedSuccess: createAction(
    ActionTypes.GetRequestsReceivedSuccess,
    props<{ receivedRequests: User[] }>()
  ),
  GetRequestsReceivedFail: createAction(
    ActionTypes.GetRequestsReceivedFail,
    props<{ error: string }>()
  )
};
