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
        public const string InternalServerError = "Internal server error";
        public const string UsersNotFound = "User(s) not found";
        public const string FollowRequestSent = "Follow request sent successfully";
        public const string NoRequestFromThisUser = "No requests from this user";
        public const string RequestAccepted = "Request accepted successfully";
        public const string RequestRejected = "Request rejected successfully";
        public const string RequestCanceled = "Request canceled successfully";
        public const string RequestNotFound = "Follow request not found";
        public const string RelationNotFound = "Relation not found";
        public const string UserUnfollowed = "User unfollowed successfully";
        public const string AlreadyFollowed = "User has already been followed";
    }
}
