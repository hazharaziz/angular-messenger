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
    public class GroupMemberRepository : IGroupMemberRepository
    {
        public MessengerContext Context;
        public GroupMemberRepository(MessengerContext messengerContext)
        {
            Context = messengerContext;
        }

        public void Add(GroupMember entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<GroupMember> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupMember> Find(Expression<Func<GroupMember, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public GroupMember Get(int groupId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupMember> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupMember> GetGroups(int userId)
        {
            throw new NotImplementedException();
        }

        public void Remove(GroupMember entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<GroupMember> entities)
        {
            throw new NotImplementedException();
        }
    }
}
