using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServerTests.TestData
{
    public static class Messages
    {
        public static Message IsaacMessage1 = new Message()
        {
            MessageId = 1,
            ComposerId = Users.Isaac.Id,
            ComposerName = Users.Isaac.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 10, 10)
        };

        public static Message IsaacMessage2 = new Message()
        {
            MessageId = 2,
            ComposerId = Users.Isaac.Id,
            ComposerName = Users.Isaac.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 10, 11)
        };

        public static Message IsaacMessage3 = new Message()
        {
            MessageId = 3,
            ComposerId = Users.Isaac.Id,
            ComposerName = Users.Isaac.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 10, 12)
        };

        public static Message OscarMessage1 = new Message()
        {
            MessageId = 4,
            ComposerId = Users.Oscar.Id,
            ComposerName = Users.Oscar.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 10, 13)
        };

        public static Message OscarMessage2 = new Message()
        {
            MessageId = 5,
            ComposerId = Users.Oscar.Id,
            ComposerName = Users.Oscar.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 10, 14)
        };

        public static Message OscarMessage3 = new Message()
        {
            MessageId = 6,
            ComposerId = Users.Oscar.Id,
            ComposerName = Users.Oscar.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 10, 15)
        };

        public static Message AdamMessage1 = new Message()
        {
            MessageId = 7,
            ComposerId = Users.Adam.Id,
            ComposerName = Users.Adam.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 10, 16)
        };

        public static Message AdamMessage2 = new Message()
        {
            MessageId = 8,
            ComposerId = Users.Adam.Id,
            ComposerName = Users.Adam.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 10, 17)
        };

        public static Message AdamMessage3 = new Message()
        {
            MessageId = 9,
            ComposerId = Users.Adam.Id,
            ComposerName = Users.Adam.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 10, 18)
        };

        public static Message PatricMessage1 = new Message()
        {
            MessageId = 10,
            ComposerId = Users.Patrick.Id,
            ComposerName = Users.Patrick.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 10, 19)
        };

        public static Message PatricMessage2 = new Message()
        {
            MessageId = 11,
            ComposerId = Users.Patrick.Id,
            ComposerName = Users.Patrick.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 10, 20)
        };

        public static Message PatricMessage3 = new Message()
        {
            MessageId = 12,
            ComposerId = Users.Patrick.Id,
            ComposerName = Users.Patrick.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 10, 21)
        };

        public static Message NicoMessage1 = new Message()
        {
            MessageId = 13,
            ComposerId = Users.Nico.Id,
            ComposerName = Users.Nico.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 10, 22)
        };

        public static Message NicoMessage2 = new Message()
        {
            MessageId = 14,
            ComposerId = Users.Nico.Id,
            ComposerName = Users.Nico.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 10, 23)
        };

        public static Message NicoMessage3 = new Message()
        {
            MessageId = 15,
            ComposerId = Users.Nico.Id,
            ComposerName = Users.Nico.Name,
            ReplyToId = 0,
            Text = "hello",
            DateTime = new DateTime(2020, 10, 24)
        };

        public static Message Null = null;
    }
}
