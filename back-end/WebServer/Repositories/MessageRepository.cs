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
    public class MessageRepository : IMessageRepository
    {
        public MessengerContext Context;

        public MessageRepository(MessengerContext messengerContext)
        {
            Context = messengerContext;
        }

        public Message Get(int id)
            => Context.Set<Message>().Find(id);

        public IEnumerable<Message> GetAll()
            => Context.Set<Message>().ToList();

        public IEnumerable<Message> Find(Expression<Func<Message, bool>> predicate)
            => Context.Set<Message>().Where(predicate);

        public void Add(Message entity)
            => Context.Set<Message>().Add(entity);

        public void AddRange(IEnumerable<Message> entities)
            => Context.Set<Message>().AddRange(entities);

        public void Remove(Message entity)
            => Context.Set<Message>().Remove(entity);

        public void RemoveRange(IEnumerable<Message> entities)
            => Context.Set<Message>().RemoveRange(entities);
    }
}
