using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServerTests.TestData
{
    public static class DirectMessages
    {

        public static DirectMessage DirectMessage1 = new DirectMessage()
        {
            DirectMessageId = 1,
            DirectId = 1,
            ComposerId = Users.Adam.Id,
            ReplyToId = 0,
            ComposerName = Users.Adam.Name,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 1)
        };

        public static DirectMessage DirectMessage2 = new DirectMessage()
        {
            DirectMessageId = 2,
            DirectId = 2,
            ComposerId = Users.Nico.Id,
            ReplyToId = 0,
            ComposerName = Users.Nico.Name,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 2)
        };

        public static DirectMessage DirectMessage3 = new DirectMessage()
        {
            DirectMessageId = 3,
            DirectId = 2,
            ComposerId = Users.Patrick.Id,
            ReplyToId = 0,
            ComposerName = Users.Patrick.Name,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 3)
        };

        public static DirectMessage DirectMessage4 = new DirectMessage()
        {
            DirectMessageId = 4,
            DirectId = 2,
            ComposerId = Users.Patrick.Id,
            ReplyToId = 0,
            ComposerName = Users.Patrick.Name,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 4)
        };

        public static DirectMessage DirectMessage5 = new DirectMessage()
        {
            DirectMessageId = 5,
            DirectId = 5,
            ComposerId = Users.Isaac.Id,
            ReplyToId = 0,
            ComposerName = Users.Isaac.Name,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 5)
        };

        public static DirectMessage DirectMessage6 = new DirectMessage()
        {
            DirectMessageId = 6,
            DirectId = 5,
            ComposerId = Users.Oscar.Id,
            ReplyToId = 0,
            ComposerName = Users.Oscar.Name,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 6)
        };

        public static DirectMessage DirectMessage7 = new DirectMessage()
        {
            DirectMessageId = 7,
            DirectId = 7,
            ComposerId = Users.Nico.Id,
            ReplyToId = 0,
            ComposerName = Users.Nico.Name,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 7)
        };

        public static DirectMessage DirectMessage8 = new DirectMessage()
        {
            DirectMessageId = 8,
            DirectId = 8,
            ComposerId = Users.Patrick.Id,
            ReplyToId = 0,
            ComposerName = Users.Patrick.Name,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 8)
        };

        public static DirectMessage DirectMessage9 = new DirectMessage()
        {
            DirectMessageId = 9,
            DirectId = 9,
            ComposerId = Users.Isaac.Id,
            ReplyToId = 0,
            ComposerName = Users.Isaac.Name,
            Text = "hello",
            DateTime = new DateTime(2020, 2, 9)
        };

        public static DirectMessage DirectMessage10 = new DirectMessage()
        {
            DirectMessageId = 10,
            DirectId = 10,
            ComposerId = 0,
            ReplyToId = 0,
            ComposerName = "Deleted Account",
            Text = "hello",
            DateTime = new DateTime(2020, 2, 10)
        };

        public static DirectMessage Null = null;

    }
}
