using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServer.Interfaces
{
    public interface IGroupMemberRepository
    {
        List<GroupMember> GetAll();
        List<GroupMember> Find(Expression<Func<GroupMember, bool>> predicate);
        void Add(GroupMember entity);
        void AddRange(IEnumerable<GroupMember> entities);
        void Remove(GroupMember entity);
        void RemoveRange(IEnumerable<GroupMember> entities);
        List<int> GetUserGroups(int userId);
        List<int> GetGroupMembers(int groupId);
        GroupMember GetGroupMember(int groupId, int memberId);
        bool IsMemberOfGroup(int userId, int groupId);
    }
}
