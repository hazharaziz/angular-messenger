export const ActionTypes = {
  LoginRequest: '[Login Screen] Login Request',
  LoginSuccess: '[Login Effects] Login Success',
  LoginFail: '[Login Effects] Login Fail',
  SignUpRequest: '[SignUp Screen] SignUp Request',
  SignUpSuccess: '[SignUp Effects] SignUp Success',
  SignUpFail: '[SignUp Effects] SignUp Fail',
  ClearCredentials: 'Clear Credentials',
  GetChatMessagesRequest: '[GeneralChat Screen] Get Messages Request',
  GetChatMessagesSuccess: '[GetGeneralChatMessages Effects] Get Messages Success',
  GetChatMessagesFail: '[GetGeneralChatMessages Fail] Get Messages Fail',
  SendMessageRequest: '[GeneralChat Screen] Send Message Request',
  SendMessageFail: '[SendMessage Effects] Send Message Fail',
  EditMessageRequest: '[GeneralChat Screen] Edit Message Request',
  EditMessageFail: '[EditMessage Effects] Edit Message Fail',
  DeleteMessageRequest: '[GeneralChat Screen] Delete Message Request',
  DeleteMessageFail: '[DeleteMessage Effects] Delete Message Fail',
  GetProfileRequest: '[Profile Screen] Get Profile Request',
  GetProfileSuccess: '[GetProfile Effects] Get Profile Success',
  GetProfileFail: '[GetProfile Effects] Get Profile Fail',
  EditProfileRequest: '[EditProfile Screen] Edit Profile Request',
  EditProfileSuccess: '[EditProfile Effects] Edit Profile Success',
  EditProfileFail: '[EditProfile Effects] Edit Profile Fail',
  ChangePasswordRequest: '[ChangePassword Screen] Change Password Request',
  ChangePasswordSuccess: '[ChangePassword Effects] Change Password Success',
  ChangePasswordFail: '[ChangePassword Effects] Change Password Fail',
  DeleteAccountRequest: '[Profile Screen] Delete Account Request',
  DeleteAccountSuccess: '[DeleteAccount Effects] Delete Account Success',
  DeleteAccountFail: '[DeleteAccount Effects] Delete Account Fail',
  GetFollowersRequest: '[Followers Screen] Get Followers Request',
  GetFollowersSuccess: '[Followers Effects] Get Followers Success',
  GetFollowersFail: '[Followers Effects] Get Followers Fail',
  RemoveFollowerRequest: '[Followers Screen] Remove Follower Request',
  RemoveFollowerFail: '[RemoveFollower Effects] Remove Follower Fail',
  GetFollowingsRequest: '[Followings Screen] Get Followings Request',
  GetFollowingsSuccess: '[Followings Effects] Get Followings Success',
  GetFollowingsFail: '[Followings Effects] Get Followings Fail',
  UnfollowRequest: '[Followings Screen] Unfollow Request',
  UnfollowFail: '[Unfollow Effects] Unfollow Fail',
  GetRequestsSentRequest: '[SentRequests Screen] Get Sent Requests Request',
  GetRequestsSentSuccess: '[SentRequests Effects] Get Sent Requests Success',
  GetRequestsSentFail: '[SentRequests Effects] Get Sent Requests Fail',
  CancelRequestRequest: '[SentRequests Screen] Cancel Request Request',
  CancelRequestFail: '[SentRequests Effects] Cancel Request Fail',
  GetRequestsReceivedRequest: '[ReceivedRequests Screen] Get Received Requests Request',
  GetRequestsReceivedSuccess: '[ReceivedRequests Effects] Get Received Requests Success',
  GetRequestsReceivedFail: '[ReceivedRequests Effects] Get Received Requests Fail',
  AcceptRequestRequest: '[ReceivedRequests Screen] Accept Request Request',
  AcceptRequestFail: '[ReceivedRequests Effects] Accept Request Fail',
  RejectRequestRequest: '[ReceivedRequests Screen] Reject Request Request',
  RejectRequestFail: '[ReceivedRequests Effects] Reject Request Fail',
  FollowRequest: '[Search Screen] Follow Request',
  FollowFail: '[Search Effects] Follow Fail',
  SearchRequest: '[Search Screen] Search Request',
  SearchSuccess: '[Search Effects] Search Success',
  SearchFail: '[Search Effects] Search Fail',
  RemoveSearchItem: '[Search Screen] Remove Search Item',
  GetGroupsRequest: '[Groups Screen] Get Groups Request',
  GetGroupsSuccess: '[Groups Effects] Get Groups Success',
  GetGroupsFail: '[Groups Effects] Get Groups Fail',
  GetGroupInfoRequest: '[GroupInfo Screen] Get Group Info Request',
  GetGroupInfoSuccess: '[GroupInfo Effects] Get Group Info Success',
  GetGroupInfoFail: '[GroupInfo Effects] Get Group Info Fail',
  GetAvailableFriendsRequest: '[AddMember Screen] Get Available Friends Request',
  GetAvailableFriendsSuccess: '[GetAvailableFriends Effects] Get Available Friends Success',
  GetAvailableFriendsFail: '[GetAvailableFriends Effects] Get Available Friends Fail',
  ClearAvailableFriends: '[AddMember Screen] Clear Available Friends',
  CreateGroupRequest: '[CreateGroup Screen] Create Group Request',
  CreateGroupFail: '[CreateGroup Effects] Create Group Fail',
  EditGroupRequest: '[EditGroup Screen] Edit Group Request',
  EditGroupFail: '[EditGroup Effects] Edit Group Fail',
  DeleteGroupRequest: '[Groups Screen] Delete Group Request',
  DeleteGroupFail: '[DeleteGroup Effects] Delete Group Fail',
  AddMemberToGroupRequest: '[AddMember Screen] Add Member To Group Request',
  AddMemberToGroupFail: '[AddMemberToGroup Effects] Add Member To Group Fail',
  RemoveMemberFromGroupRequest: '[GroupInfo Screen] Remove Member From Group Request',
  RemoveMemberFromGroupSuccess: '[RemoveMemberToGroup Effects] Remove Member From Group Success',
  RemoveMemberFromGroupFail: '[RemoveMemberToGroup Effects] Remove Member From Group Fail',
  GetGroupMessagesRequest: '[GroupChat Screen] Get Group Messages Request',
  GetGroupMessagesSuccess: '[GetGroupMessages Effects] Get Group Messages Success',
  GetGroupMessagesFail: '[GetGroupMessages Effects] Get Group Messages Fail',
  SendGroupMessageRequest: '[GroupChat Screen] Send Group Messages Request',
  SendGroupMessagesFail: '[SendGroupMessages Effects] Send Group Messages Fail',
  EditGroupMessagesRequest: '[GroupChat Screen] Edit Group Messages Request',
  EditGroupMessageFail: '[EditGroupMessages Effects] Edit Group Messages Fail',
  DeleteGroupMessagesRequest: '[GroupChat Screen] Delete Group Messages Request',
  DeleteGroupMessageFail: '[DeleteGroupMessages Effects] Delete Group Messages Fail',
  ClearGroupChatHistoryRequest: '[GroupChat Screen] Clear Group Chat History Request',
  ClearGroupChatHistoryFail: '[ClearGroupChatHistory Effects] Clear Group Chat History Fail',
  LeaveGroupRequest: '[Groups Screen] Leave Group Request',
  LeaveGroupFail: '[LeaveGroup Effects] Leave Group Fail',
  GetDirectsRequest: '[Directs Screen] Get Directs Screen Request',
  GetDirectsSuccess: '[GetDirects Effects] Get Directs Screen Success',
  GetDirectsFail: '[GetDirects Effects] Get Directs Screen Fail',
  GetDirectMessageRequest: '[DirectChat Screen] Get Direct Message Screen Request',
  GetDirectMessageSuccess: '[GetDirectMessage Effects] Get Direct Message Screen Success',
  GetDirectMessageFail: '[GetDirectMessage Effects] Get Direc tMessage Screen Fail',
  SendDirectMessageRequest: '[DirectChat Screen] Send Direct Message Request',
  SendDirectMessageSuccess: '[SendDirectMessage Screen] Send Direct Message Succes',
  SendDirectMessageFail: '[SendDirectMessage Screen] Send Direct Message Fail',
  EditDirectMessageRequest: '[DirectChat Screen] Edit Direct Message Request',
  EditDirectMessageSuccess: '[EditDirectMessage Screen] Edit Direct Message Succes',
  EditDirectMessageFail: '[EditDirectMessage Screen] Edit Direct Message Fail',
  DeleteDirectMessageRequest: '[DirectChat Screen] Delete Direct Message Request',
  DeleteDirectMessageSuccess: '[DeleteDirectMessage Screen] Delete Direct Message Succes',
  DeleteDirectMessageFail: '[DeleteDirectMessage Screen] Delete Direct Message Fail',
  ClearDirectChatHistoryRequest: '[DirectChat Screen] Clear Direct Chat History Request',
  ClearDirectChatHistorySuccess:
    '[ClearDirectChatHistory Effects] Clear Direct Chat History Success',
  ClearDirectChatHistoryFail: '[ClearDirectChatHistory Effects] Clear Direct Chat History Fail',
  RemoveDirectRequest: '[Directs Screen] Remove Direct Request',
  RemoveDirectSuccess: '[RemoveDirects Effects] Remove Direct Success',
  RemoveDirectFail: '[RemoveDirects Effects] Remove Direct Fail'
};
