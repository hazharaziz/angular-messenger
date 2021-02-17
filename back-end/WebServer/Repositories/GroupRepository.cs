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
    public class GroupRepository : IGroupRepository
    {
        public MessengerContext Context;
        public GroupRepository(MessengerContext messengerContext)
        {
            Context = messengerContext;
        }

        public void Add(Group entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Group> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> Find(Expression<Func<Group, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Group Get(int groupId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetGroups(int userId)
        {
            throw new NotImplementedException();
        }

        public void Remove(Group entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Group> entities)
        {
            throw new NotImplementedException();
        }
    }
}
