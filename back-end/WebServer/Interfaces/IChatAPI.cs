using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServer.Interfaces
{
    public interface IChatAPI
    {
        List<Message> FetchMessages();
        List<Message> FetchFriendsMessages(int userId);
        void AddMessage(Message message);
        void EditMessage(int id, Message message);
        User GetCurrentUser(string username);
    }
}
