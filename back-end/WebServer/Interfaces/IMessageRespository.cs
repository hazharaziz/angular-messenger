using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebServer.Models;

namespace WebServer.Interfaces
{
    interface IMessageRespository
    {
        Message Get(int id);
        IEnumerable<Message> GetAll();
        IEnumerable<Message> Find(Expression<Func<Message, bool>> predicate);
        void Add(Message entity);
        void AddRange(IEnumerable<Message> entities);
        void Remove(Message entity);
        void RemoveRange(IEnumerable<Message> entities);
    }
}
