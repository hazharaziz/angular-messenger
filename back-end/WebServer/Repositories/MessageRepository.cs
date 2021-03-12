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
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(MessengerContext messengerContext): base(messengerContext) { }

        public List<Message> GetUserMessages(int userId)
            => Find(m => m.ComposerId == userId);

        public Message Get(int messageId)
            => Find(m => m.MessageId == messageId).FirstOrDefault();
    }
}
