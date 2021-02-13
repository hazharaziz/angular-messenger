using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Interfaces
{
    public interface IChatAPI
    {
        List<Message> FetchMessages();
        List<ResponseMessage> FetchFriendsMessages(int userId);
        List<ResponseMessage> FetchFriendsMessages(string username);
        void AddMessage(Message message);
        void EditMessage(int id, Message message);
        void DeleteMessage(int id);
        User GetCurrentUser(string username);
    }
}
