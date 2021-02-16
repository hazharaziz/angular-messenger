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
        public const string Conflict = "Resource already exists";
        public const string MessageCreatedSuccess = "Message created successfully";
        public const string MessageEditedSuccess = "Message edited successfully";
        public const string InternalServerError = "Internal server error";
    }
}
