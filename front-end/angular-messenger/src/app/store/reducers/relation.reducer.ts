import { state } from '@angular/animations';
import { createReducer, on } from '@ngrx/store';
import { Relation } from 'src/app/models/data/relation.model';
import { User } from 'src/app/models/data/user.model';
import { RelationActions } from '../actions/relation.actinos';

const initialState: Relation = {};

export const relationReducer = createReducer(
  initialState,
  on(RelationActions.GetFollowersSucces, (state: Relation, payload: { followers: User[] }) => ({
    ...state,
    followers: payload.followers
  }))
);
