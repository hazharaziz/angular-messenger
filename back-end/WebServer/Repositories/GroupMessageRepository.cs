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
    public class GroupMessageRepository : IGroupMessageRepository
    {
        public MessengerContext Context;
        public GroupMessageRepository(MessengerContext messengerContext)
        {
            Context = messengerContext;
        }

        public void Add(GroupMessage groupMessage)
        {
            throw new NotImplementedException();
        }

        public void AddRange(List<GroupMessage> groupMessages)
        {
            throw new NotImplementedException();
        }

        public List<GroupMessage> Find(Expression<Func<GroupMessage, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public GroupMessage Get(int groupMessageId)
        {
            throw new NotImplementedException();
        }

        public List<GroupMessage> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<GroupMessage> GetGroupMessages(int userId)
        {
            throw new NotImplementedException();
        }

        public void Remove(GroupMessage groupMessage)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(List<GroupMessage> groupMessages)
        {
            throw new NotImplementedException();
        }
    }
}
