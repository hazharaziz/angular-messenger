using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServerTests.TestData
{
    public static class Relations
    {
        public static Follower Relation1 = new Follower()
        {
            RelationId = 1,
            UserId = Users.Adam.Id,
            FollowerId = Users.Patrick.Id,
            Pending = 0
        };

        public static Follower Relation2 = new Follower()
        {
            RelationId = 2,
            UserId = Users.Patrick.Id,
            FollowerId = Users.Isaac.Id,
            Pending = 1
        };

        public static Follower Relation3 = new Follower()
        {
            RelationId = 3,
            UserId = Users.Nico.Id,
            FollowerId = Users.Adam.Id,
            Pending = 0
        };

        public static Follower Relation4 = new Follower()
        {
            RelationId = 4,
            UserId = Users.Oscar.Id,
            FollowerId = Users.Patrick.Id,
            Pending = 1
        };

        public static Follower Relation5 = new Follower()
        {
            RelationId = 5,
            UserId = Users.Adam.Id,
            FollowerId = Users.Nico.Id,
            Pending = 0
        };

        public static Follower Relation6 = new Follower()
        {
            RelationId = 6,
            UserId = Users.Isaac.Id,
            FollowerId = Users.Adam.Id,
            Pending = 1
        };

        public static Follower Relation7 = new Follower()
        {
            RelationId = 7,
            UserId = Users.Adam.Id,
            FollowerId = Users.Isaac.Id,
            Pending = 0
        };

        public static Follower Relation8 = new Follower()
        {
            RelationId = 8,
            UserId = Users.Nico.Id,
            FollowerId = Users.Patrick.Id,
            Pending = 1
        };

        public static Follower Relation9 = new Follower()
        {
            RelationId = 9,
            UserId = Users.Oscar.Id,
            FollowerId = Users.Nico.Id,
            Pending = 0
        };

        public static Follower Relation10 = new Follower()
        {
            RelationId = 10,
            UserId = Users.Adam.Id,
            FollowerId = Users.Oscar.Id,
            Pending = 1
        };

        public static Follower Relation11 = new Follower()
        {
            RelationId = 11,
            UserId = Users.Adam.Id,
            FollowerId = 0,
            Pending = 0
        };

        public static Follower Relation12 = new Follower()
        {
            RelationId = 12,
            UserId = 0,
            FollowerId = Users.Oscar.Id,
            Pending = 0
        };
    }
}
