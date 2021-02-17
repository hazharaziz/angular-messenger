using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServer.Interfaces
{
    public interface IGroupRepository
    {
        IEnumerable<Group> GetAll();
        IEnumerable<Group> GetGroups(int userId);
        Group Get(int groupId);
        IEnumerable<Group> Find(Expression<Func<Group, bool>> predicate);
        void Add(Group entity);
        void AddRange(IEnumerable<Group> entities);
        void Remove(Group entity);
        void RemoveRange(IEnumerable<Group> entities);
    }
}
