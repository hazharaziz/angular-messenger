import { createReducer, on } from '@ngrx/store';
import { User } from 'src/app/models/data/user.model';
import { ProfileActions } from '../actions/profile.actions';

const initialState: User = {};

export const profileReducer = createReducer(
  initialState,
  on(ProfileActions.GetProfileSuccess, (state: User, action: User) => action)
);
