using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Interfaces
{
    public interface IDirectService
    {
        Response<List<DirectModel>> GetUserDirects(int userId);
        Response<List<DirectMessage>> GetDirectMessages(int userId, int targetId);
        Response<string> SendDirectMessage(int userId, int targetId, DirectMessage directMessage);
        Response<string> EditDirectMessage(int userId, int directMessageId, DirectMessage editedMessage);
        Response<string> DeleteDirectMessage(int userId, int directMessageId);
        Response<string> DeleteDirect(int userId, int directId);
        Response<string> DeleteDirectHistory(int userId, int targetId);
    }
}
