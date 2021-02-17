using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServer.Interfaces
{
    public interface IGroupMessageRepository
    {
        List<GroupMessage> GetAll();
        List<GroupMessage> GetGroupMessages(int userId);
        GroupMessage Get(int groupMessageId);
        List<GroupMessage> Find(Expression<Func<GroupMessage, bool>> predicate);
        void Add(GroupMessage groupMessage);
        void AddRange(List<GroupMessage> groupMessages);
        void Remove(GroupMessage groupMessage);
        void RemoveRange(List<GroupMessage> groupMessages);
    }
}
