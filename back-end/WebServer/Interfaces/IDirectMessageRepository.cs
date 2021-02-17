using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServer.Interfaces
{
    public interface IDirectMessageRepository
    {
        List<DirectMessage> GetAll();
        List<DirectMessage> GetDirectMessages(int userId);
        DirectMessage Get(int directMessageId);
        List<DirectMessage> Find(Expression<Func<DirectMessage, bool>> predicate);
        void Add(DirectMessage directMessage);
        void AddRange(List<DirectMessage> directMessages);
        void Remove(DirectMessage directMessage);
        void RemoveRange(List<DirectMessage> directMessages);
    }
}
