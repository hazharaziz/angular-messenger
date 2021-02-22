using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServerTests.TestData
{
    public static class GroupMembers
    {
        public static GroupMember Record1 = new GroupMember()
        {
            MemberId = 1,
            GroupId = Groups.Group1.GroupId,
            UserId = Users.Isaac.Id
        };

        public static GroupMember Record2 = new GroupMember()
        {
            MemberId = 2,
            GroupId = Groups.Group2.GroupId,
            UserId = Users.Patrick.Id
        };

        public static GroupMember Record3 = new GroupMember()
        {
            MemberId = 3,
            GroupId = Groups.Group3.GroupId,
            UserId = Users.Oscar.Id
        };

        public static GroupMember Record4 = new GroupMember()
        {
            MemberId = 4,
            GroupId = Groups.Group1.GroupId,
            UserId = Users.Adam.Id
        };

        public static GroupMember Record5 = new GroupMember()
        {
            MemberId = 5,
            GroupId = Groups.Group1.GroupId,
            UserId = Users.Nico.Id
        };

        public static GroupMember Record6 = new GroupMember()
        {
            MemberId = 6,
            GroupId = Groups.Group2.GroupId,
            UserId = Users.Isaac.Id
        };

        public static GroupMember Record7 = new GroupMember()
        {
            MemberId = 7,
            GroupId = Groups.Group2.GroupId,
            UserId = Users.Adam.Id
        };

        public static GroupMember Null = null;

    }
}
