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
        Response<List<Message>> FetchFriendsMessages(int userId);
        Response<string> AddMessage(int userId, Message message);
        Response<string> EditMessage(int userId, int messageId, Message message);
        Response<string> DeleteMessage(int userId, int messageId);
    }
}
