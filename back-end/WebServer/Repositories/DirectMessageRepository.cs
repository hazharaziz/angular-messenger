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
    public class DirectMessageRepository : Repository<DirectMessage>, IDirectMessageRepository
    {
        public DirectMessageRepository(MessengerContext messengerContext) : base(messengerContext) { }

        public List<DirectMessage> GetDirectMessages(int directId)
            => Find(dm => dm.DirectId == directId);

        public List<DirectMessage> GetUserDirectMessages(int userId)
            => Find(dm => dm.ComposerId == userId);

        public DirectMessage Get(int directMessageId)
            => Find(dm => dm.DirectMessageId == directMessageId).FirstOrDefault();

    }
}
