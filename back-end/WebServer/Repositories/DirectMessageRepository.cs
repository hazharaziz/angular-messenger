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
    public class DirectMessageRepository : IDirectMessageRepository
    {
        public MessengerContext Context;
        public DirectMessageRepository(MessengerContext messengerContext)
        {
            Context = messengerContext;
        }

        public void Add(DirectMessage directMessage)
        {
            throw new NotImplementedException();
        }

        public void AddRange(List<DirectMessage> directMessages)
        {
            throw new NotImplementedException();
        }

        public List<DirectMessage> Find(Expression<Func<DirectMessage, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public DirectMessage Get(int directMessageId)
        {
            throw new NotImplementedException();
        }

        public List<DirectMessage> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<DirectMessage> GetDirectMessages(int userId)
        {
            throw new NotImplementedException();
        }

        public void Remove(DirectMessage directMessage)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(List<DirectMessage> directMessages)
        {
            throw new NotImplementedException();
        }
    }
}
