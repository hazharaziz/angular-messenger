import { createReducer, on } from '@ngrx/store';
import { User } from 'src/app/models/data/user.model';
import { FriendActions } from '../actions/friend.actinos';

const initialState: User[] = [];

export const friendReducer = createReducer(
  initialState,
  on(
    FriendActions.GetAvailableFriendsSuccess,
    (state: User[], payload: { friends: User[] }) => payload.friends
  ),
  on(FriendActions.ClearAvailableFriends, (state: User[]) => [])
);
