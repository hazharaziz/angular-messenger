using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebServer.Models.DBModels;


namespace WebServer.Interfaces
{
    public interface IMessageRepository
    {
        List<Message> GetAll();
        List<Message> Find(Expression<Func<Message, bool>> predicate);
        void Add(Message entity);
        void AddRange(IEnumerable<Message> entities);
        void Remove(Message entity);
        void RemoveRange(IEnumerable<Message> entities);
        List<Message> GetUserMessages(int userId);
        Message Get(int messageId);
    }
}