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

        public IEnumerable<DirectMessage> GetFollowers(int userId)
            => Find(f => (f.UserId == userId) && f.Pending == 0);
        public IEnumerable<DirectMessage> GetAll()
            => Context.Set<DirectMessage>().ToList();

        public IEnumerable<DirectMessage> Find(Expression<Func<DirectMessage, bool>> predicate)
            => Context.Set<DirectMessage>().Where(predicate);

        public void Add(DirectMessage entity)
            => Context.Set<DirectMessage>().Add(entity);

        public void AddRange(IEnumerable<DirectMessage> entities)
            => Context.Set<DirectMessage>().AddRange(entities);

        public void Remove(DirectMessage entity)
            => Context.Set<DirectMessage>().Remove(entity);

        public void RemoveRange(IEnumerable<DirectMessage> entities)
            => Context.Set<DirectMessage>().RemoveRange(entities);

        public bool HasFollower(int userId, int followerId)
        {
            bool result = false;
            DirectMessage follower = Find(f => ((f.UserId == userId) && (f.FollowerId == followerId)))
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
            DirectMessage follower = Find(f => ((f.UserId == userId) && (f.FollowerId == followerId)))
                .FirstOrDefault();
            if (follower != null)
            {
                result = follower.Pending == 1;
            }
            return result;
        }
    }
}
