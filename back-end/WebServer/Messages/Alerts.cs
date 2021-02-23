using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Messages
{
    public static class Alerts
    {
        public const string NotFound = "Resource not found";
        public const string Forbidden = "Authorization failed";
        public const string UnAuthorized = "Wrong authentication credentials";
        public const string Conflict = "Resource conflict";
        public const string MessageCreated = "Message created successfully";
        public const string MessageEdited = "Message edited successfully";
        public const string MessageDeleted = "Message deleted successfully";
        public const string InternalServerError = "Internal server error";
        public const string UsersNotFound = "User(s) not found";
        public const string FollowRequestSent = "Follow request sent successfully";
        public const string NoRequestFromThisUser = "No requests from this user";
        public const string RequestAccepted = "Request accepted successfully";
        public const string RequestRejected = "Request rejected successfully";
        public const string RequestCanceled = "Request canceled successfully";
        public const string RequestNotFound = "Follow request not found";
        public const string RelationNotFound = "Relation not found";
        public const string RelationDeleted = "relation deleted successfully";
        public const string AlreadyFollowed = "User has already been followed";
        public const string AlreadySentRequest = "Request already sent";
        public const string AlreadyIsFollower = "User is already a follower";
        public const string WrongPassword = "Wrong password";
        public const string PasswordChanged = "Password changed successfully";
        public const string UsernameExists = "Username already exists";
        public const string AccountDeleted = "Account deleted successfully";
        public const string DirectNotFound = "Direct Not Found";
        public const string MessageNotFound = "Message Not Found";
        public const string DirectDeleted = "Direct deleted successfully";
        public const string HistoryDeleted = "History cleared successfully";
        public const string NotAllowed = "Operation not allowed";
        public const string GroupCreated = "Group created successfully";
        public const string GroupEdited = "Group edited successfully";
        public const string GroupDeleted = "Group deleted successfully";
        public const string MemberAdded = "Member added to group successfully";
        public const string MemberRemoved = "Member removed from group successfully";
        public const string LeftGroup = "You left the group successfully";
        public const string AlreadyMember = "User is already member of the group";
        public const string NotFriends = "User is not your friend";
        public const string ProfileEdited = "Profile edited successfully";
        public const string SomethingWentWrong = "Something went wrong";
        public const string GroupNotFound = "Group not found";
        public const string MemberNotFound = "Member not found";
        public const string NotGroupMember = "User is not a member of this group";
    }
}
