import { createReducer, on } from '@ngrx/store';

import { Relation } from 'src/app/models/data/relation.model';
import { User } from 'src/app/models/data/user.model';
import { RelationActions } from '../actions/relation.actinos';

const initialState: Relation = {
  followers: [],
  followings: [],
  receivedRequest: [],
  sentRequests: []
};

export const relationReducer = createReducer(
  initialState,
  on(RelationActions.GetFollowersSucces, (state: Relation, payload: { followers: User[] }) => ({
    ...state,
    followers: payload.followers
  })),
  on(RelationActions.GetFollowingsSucces, (state: Relation, payload: { followings: User[] }) => ({
    ...state,
    followings: payload.followings
  })),
  on(
    RelationActions.GetRequestsSentSucces,
    (state: Relation, payload: { sentRequests: User[] }) => ({
      ...state,
      sentRequests: payload.sentRequests
    })
  ),
  on(
    RelationActions.GetRequestsReceivedSuccess,
    (state: Relation, payload: { receivedRequests: User[] }) => ({
      ...state,
      receivedRequest: payload.receivedRequests
    })
  )
);
