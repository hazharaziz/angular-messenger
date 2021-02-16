using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Interfaces
{
    public interface IChatService
    {
        List<Message> FetchMessages();
        Response<List<Message>> FetchFriendsMessages(int userId);
        Response<List<Message>> FetchFriendsMessages(string username);
        Response<string> AddMessage(Message message);
        Response<string> EditMessage(int id, Message message);
        Response<string> DeleteMessage(int id);
    }
}
