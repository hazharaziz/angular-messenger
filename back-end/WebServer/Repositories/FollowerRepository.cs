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

        public IEnumerable<Direct> GetFollowers(int userId)
            => Find(f => (f.UserId == userId) && f.Pending == 0);
        public IEnumerable<Direct> GetAll()
            => Context.Set<Direct>().ToList();

        public IEnumerable<Direct> Find(Expression<Func<Direct, bool>> predicate)
            => Context.Set<Direct>().Where(predicate);

        public void Add(Direct entity)
            => Context.Set<Direct>().Add(entity);

        public void AddRange(IEnumerable<Direct> entities)
            => Context.Set<Direct>().AddRange(entities);

        public void Remove(Direct entity)
            => Context.Set<Direct>().Remove(entity);

        public void RemoveRange(IEnumerable<Direct> entities)
            => Context.Set<Direct>().RemoveRange(entities);

        public bool HasFollower(int userId, int followerId)
        {
            bool result = false;
            Direct follower = Find(f => ((f.UserId == userId) && (f.FollowerId == followerId)))
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
            Direct follower = Find(f => ((f.UserId == userId) && (f.FollowerId == followerId)))
                .FirstOrDefault();
            if (follower != null)
            {
                result = follower.Pending == 1;
            }
            return result;
        }
    }
}
