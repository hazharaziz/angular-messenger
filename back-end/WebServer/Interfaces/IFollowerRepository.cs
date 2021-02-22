using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServer.Interfaces
{
    public interface IFollowerRepository
    {
        List<Follower> GetAll();
        List<Follower> Find(Expression<Func<Follower, bool>> predicate);
        void Add(Follower entity);
        void AddRange(IEnumerable<Follower> entities);
        void Remove(Follower entity);
        void RemoveRange(IEnumerable<Follower> entities);
        List<Follower> GetFollowers(int userId);
        List<Follower> GetFollowings(int userId);
        List<Follower> GetFollowRequests(int userId);
        Follower GetRelation(int userId, int followerId);
        Follower GetRequest(int userId, int followerId);
        bool IsFriend(int userId, int followerId);
        bool HasFollower(int userId, int followerId);
        bool HasRequestFrom(int userId, int followerId);
    }
}
