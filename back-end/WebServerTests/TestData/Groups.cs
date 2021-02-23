using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServerTests.TestData
{
    public static class Groups
    {
        public static Group Group1 = new Group()
        {
            GroupId = 1,
            GroupName = "Group 1",
            CreatorId = Users.Isaac.Id,
            CreatorName = Users.Isaac.Name,
            AddMemberAccess = 0,
            MembersCount = 3
        };

        public static Group Group2 = new Group()
        {
            GroupId = 2,
            GroupName = "Group 2",
            CreatorId = Users.Patrick.Id,
            CreatorName = Users.Patrick.Name,
            AddMemberAccess = 1,
            MembersCount = 2
        };

        public static Group Group3 = new Group()
        {
            GroupId = 3,
            GroupName = "Group 3",
            CreatorId = Users.Oscar.Id,
            CreatorName = Users.Oscar.Name,
            AddMemberAccess = 0,
            MembersCount = 1
        };

        public static Group Null = null;
    }
}
