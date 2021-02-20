using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebServer.DataContext;
using WebServer.Interfaces;
using WebServer.Models.DBModels;

namespace WebServer.Repositories
{
    public class FollowerRepository : Repository<Follower>, IFollowerRepository
    {
        public FollowerRepository(MessengerContext messengerContext) : base(messengerContext) { }

        public List<Follower> GetFollowers(int userId)
            => Find(f => (f.UserId == userId) && f.Pending == 0);

        public List<Follower> GetFollowings(int userId)
            => Find(f => (f.FollowerId == userId) && f.Pending == 0);

        public List<Follower> GetFollowRequests(int userId)
            => Find(f => (f.UserId == userId) && f.Pending == 1);

        public bool IsFriend(int userId, int followerId)
            => HasFollower(userId, followerId) && HasFollower(followerId, userId);

        public bool HasFollower(int userId, int followerId)
        {
            bool result = false;
            Follower follower = Find(f => ((f.UserId == userId) && (f.FollowerId == followerId)))
                .FirstOrDefault();
            if (follower != null)
            {
                result = follower.Pending == 0;
            }
            return result;
        }

        public bool HasRequestFrom(int userId, int followerId)
        {
            bool result = false;
            Follower follower = Find(f => ((f.UserId == userId) && (f.FollowerId == followerId)))
                .FirstOrDefault();
            if (follower != null)
            {
                result = follower.Pending == 1;
            }
            return result;
        }
    }
}
