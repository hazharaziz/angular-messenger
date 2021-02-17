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

        public IEnumerable<Group> GetFollowers(int userId)
            => Find(f => (f.UserId == userId) && f.Pending == 0);
        public IEnumerable<Group> GetAll()
            => Context.Set<Group>().ToList();

        public IEnumerable<Group> Find(Expression<Func<Group, bool>> predicate)
            => Context.Set<Group>().Where(predicate);

        public void Add(Group entity)
            => Context.Set<Group>().Add(entity);

        public void AddRange(IEnumerable<Group> entities)
            => Context.Set<Group>().AddRange(entities);

        public void Remove(Group entity)
            => Context.Set<Group>().Remove(entity);

        public void RemoveRange(IEnumerable<Group> entities)
            => Context.Set<Group>().RemoveRange(entities);

        public bool HasFollower(int userId, int followerId)
        {
            bool result = false;
            Group follower = Find(f => ((f.UserId == userId) && (f.FollowerId == followerId)))
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
            Group follower = Find(f => ((f.UserId == userId) && (f.FollowerId == followerId)))
                .FirstOrDefault();
            if (follower != null)
            {
                result = follower.Pending == 1;
            }
            return result;
        }
    }
}
