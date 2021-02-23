using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServerTests.TestData
{
    public static class GroupMessages
    {
        public static GroupMessage GroupMessage1 = new GroupMessage()
        {
            GroupMessageId = 1,
            GroupId = 1,
            ComposerId = Users.Adam.Id,
            ComposerName = Users.Adam.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 1)
        };

        public static GroupMessage GroupMessage2 = new GroupMessage()
        {
            GroupMessageId = 2,
            GroupId = 1,
            ComposerId = Users.Nico.Id,
            ComposerName = Users.Nico.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 2)
        };

        public static GroupMessage GroupMessage3 = new GroupMessage()
        {
            GroupMessageId = 3,
            GroupId = 1,
            ComposerId = Users.Isaac.Id,
            ComposerName = Users.Isaac.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 3)
        };

        public static GroupMessage GroupMessage4 = new GroupMessage()
        {
            GroupMessageId = 4,
            GroupId = 2,
            ComposerId = Users.Patrick.Id,
            ComposerName = Users.Patrick.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 4)
        };

        public static GroupMessage GroupMessage5 = new GroupMessage()
        {
            GroupMessageId = 5,
            GroupId = 2,
            ComposerId = Users.Isaac.Id,
            ComposerName = Users.Isaac.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 5)
        };

        public static GroupMessage GroupMessage6 = new GroupMessage()
        {
            GroupMessageId = 6,
            GroupId = 2,
            ComposerId = Users.Patrick.Id,
            ComposerName = Users.Patrick.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 6)
        };

        public static GroupMessage GroupMessage7 = new GroupMessage()
        {
            GroupMessageId = 7,
            GroupId = 3,
            ComposerId = Users.Oscar.Id,
            ComposerName = Users.Oscar.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 7)
        };

        public static GroupMessage GroupMessage8 = new GroupMessage()
        {
            GroupMessageId = 8,
            GroupId = 3,
            ComposerId = Users.Oscar.Id,
            ComposerName = Users.Oscar.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 8)
        };

        public static GroupMessage Null = null;
    }
}
