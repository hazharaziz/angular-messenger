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
    public class FollowerRepository : IFollowerRepository
    {
        public MessengerContext Context;
        public FollowerRepository(MessengerContext messengerContext)
        {
            Context = messengerContext;
        }

        public List<Follower> GetFollowers(int userId)
            => Find(f => (f.UserId == userId) && f.Pending == 0);
        public List<Follower> GetAll()
            => Context.Set<Follower>().ToList();

        public List<Follower> Find(Expression<Func<Follower, bool>> predicate)
            => Context.Set<Follower>().Where(predicate).ToList();

        public void Add(Follower entity)
            => Context.Set<Follower>().Add(entity);

        public void AddRange(List<Follower> entities)
            => Context.Set<Follower>().AddRange(entities);
        public void Remove(Follower entity)
            => Context.Set<Follower>().Remove(entity);

        public void RemoveRange(List<Follower> entities)
            => Context.Set<Follower>().RemoveRange(entities);

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
