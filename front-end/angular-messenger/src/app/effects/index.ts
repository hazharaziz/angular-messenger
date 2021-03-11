import { LoginEffects } from './auth-effects/login.effects';
import { SignUpEffects } from './auth-effects/sign-up.effects';
import { DeleteMessageEffects } from './general-chat-effects/delete-message.effects';
import { EditMessageEffects } from './general-chat-effects/edit-message.effects';
import { GetGeneralChatMessagesEffects } from './general-chat-effects/get-general-chat-messages.effects';
import { SendMessageEffects } from './general-chat-effects/send-message.effects';
import { CreateGroupEffects } from './group-effects/create-group.effects';
import { GetAvailableFriendsEffects } from './group-effects/get-available-friends.effects';
import { GetGroupInfoEffects } from './group-effects/get-group-info.effects';
import { GetGroupsEffects } from './group-effects/get-groups.effects';
import { ChangePasswordEffects } from './profile-effects/change-password.effects';
import { DeleteAccountEffects } from './profile-effects/delete-account.effects';
import { EditProfileEffects } from './profile-effects/edit-profile.effects';
import { GetProfileEffects } from './profile-effects/get-profile.effects';
import { AcceptRequestEffects } from './relation-effects/accept-request.effects';
import { CancelRequestEffects } from './relation-effects/cancel-request.effects';
import { FollowEffects } from './relation-effects/follow.effects';
import { GetFollowersEffects } from './relation-effects/get-followers.effects';
import { GetFollowingsEffects } from './relation-effects/get-followings.effects';
import { GetRequestsReceivedEffects } from './relation-effects/get-requests-received.effects';
import { GetRequestsSentEffects } from './relation-effects/get-requests-sent.effects';
import { RejectRequestEffects } from './relation-effects/reject-request.effects';
import { RemoveFollowerEffects } from './relation-effects/remove-follower.effects';
import { UnfollowEffects } from './relation-effects/unfollow.effects';
import { SearchEffects } from './search-effects/search.effects';

export const effects = [
  LoginEffects,
  SignUpEffects,
  GetGeneralChatMessagesEffects,
  SendMessageEffects,
  EditMessageEffects,
  DeleteMessageEffects,
  GetProfileEffects,
  EditProfileEffects,
  ChangePasswordEffects,
  DeleteAccountEffects,
  GetFollowersEffects,
  RemoveFollowerEffects,
  GetFollowingsEffects,
  UnfollowEffects,
  GetRequestsSentEffects,
  AcceptRequestEffects,
  CancelRequestEffects,
  GetRequestsReceivedEffects,
  RejectRequestEffects,
  FollowEffects,
  SearchEffects,
  GetGroupsEffects,
  GetGroupInfoEffects,
  GetAvailableFriendsEffects,
  CreateGroupEffects
];
