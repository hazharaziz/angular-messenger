using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServer.Interfaces
{
    public interface IGroupMemeberRepository
    {
        IEnumerable<GroupMember> GetAll();
        IEnumerable<GroupMember> GetGroups(int userId);
        GroupMember Get(int groupId);
        IEnumerable<GroupMember> Find(Expression<Func<GroupMember, bool>> predicate);
        void Add(GroupMember entity);
        void AddRange(IEnumerable<GroupMember> entities);
        void Remove(GroupMember entity);
        void RemoveRange(IEnumerable<GroupMember> entities);
    }
}
