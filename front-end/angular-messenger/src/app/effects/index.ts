import { LoginEffects } from './auth-effects/login.effects';
import { SignUpEffects } from './auth-effects/sign-up.effects';
import { ClearDirectChatHistoryEffects } from './direct-effects/clear-direct-chat-history.effects';
import { DeleteDirectMessageEffects } from './direct-effects/delete-direct-message.effects';
import { EditDirectMessageEffects } from './direct-effects/edit-direct-message.effects';
import { GetDirectMessagesEffects } from './direct-effects/get-direct-messages.effects';
import { GetDirectsEffects } from './direct-effects/get-directs.effects';
import { RemoveDirectEffects } from './direct-effects/remove-direct.effects';
import { SendDirectMessageEffects } from './direct-effects/send-direct-message.effects';
import { DeleteMessageEffects } from './general-chat-effects/delete-message.effects';
import { EditMessageEffects } from './general-chat-effects/edit-message.effects';
import { GetGeneralChatMessagesEffects } from './general-chat-effects/get-general-chat-messages.effects';
import { SendMessageEffects } from './general-chat-effects/send-message.effects';
import { AddMemberToGroupEffects } from './group-effects/add-member-to-group.effects';
import { ClearGroupChatHistoryEffects } from './group-effects/clear-group-chat-history.effects';
import { CreateGroupEffects } from './group-effects/create-group.effects';
import { DeleteGroupMessageEffects } from './group-effects/delete-group-message.effects';
import { DeleteGroupEffects } from './group-effects/delete-group.effects';
import { EditGroupMessageEffects } from './group-effects/edit-group-message.effects';
import { EditGroupEffects } from './group-effects/edit-group.effects';
import { GetAvailableFriendsEffects } from './group-effects/get-available-friends.effects';
import { GetGroupInfoEffects } from './group-effects/get-group-info.effects';
import { GetGroupMessagesEffects } from './group-effects/get-group-messages.effects';
import { GetGroupsEffects } from './group-effects/get-groups.effects';
import { LeaveGroupEffects } from './group-effects/leave-group.effects';
import { RemoveMemberFromGroupEffects } from './group-effects/remove-member-from-group.effects';
import { SendGroupMessageEffects } from './group-effects/send-group-message.effects';
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
  CreateGroupEffects,
  EditGroupEffects,
  DeleteGroupEffects,
  AddMemberToGroupEffects,
  RemoveMemberFromGroupEffects,
  GetGroupMessagesEffects,
  SendGroupMessageEffects,
  EditGroupMessageEffects,
  DeleteGroupMessageEffects,
  ClearGroupChatHistoryEffects,
  LeaveGroupEffects,
  GetDirectsEffects,
  GetDirectMessagesEffects,
  SendDirectMessageEffects,
  EditDirectMessageEffects,
  DeleteDirectMessageEffects,
  ClearDirectChatHistoryEffects,
  RemoveDirectEffects
];
