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
        IEnumerable<Group> GetFollowers(int userId);
        IEnumerable<Group> GetAll();
        IEnumerable<Group> Find(Expression<Func<Group, bool>> predicate);
        void Add(Group entity);
        void AddRange(IEnumerable<Group> entities);
        void Remove(Group entity);
        void RemoveRange(IEnumerable<Group> entities);
        bool HasFollower(int userId, int followerId);
        bool HasRequestFrom(int userId, int followerId);
    }
}
