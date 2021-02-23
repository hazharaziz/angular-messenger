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
    public class GroupMessageRepository : Repository<GroupMessage>, IGroupMessageRepository
    {
        public GroupMessageRepository(MessengerContext messengerContext): base(messengerContext) { }

        public List<GroupMessage> GetGroupMessages(int groupId)
            => Find(gm => gm.GroupId == groupId);

        public GroupMessage Get(int groupMessageId)
            => Find(gm => gm.GroupMessageId == groupMessageId).FirstOrDefault();
    }
}
