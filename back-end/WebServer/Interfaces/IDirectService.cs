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
        Response<List<DirectMessage>> GetDirectMessages(int directId);
        Response<string> SendDirectMessage(int userId, int targetId, DirectMessage directMessage);
        Response<string> EditDirectMessage(int directMessageId, DirectMessage editedMessage);
        Response<string> DeleteDirectMessage(int directMessageId);
        Response<string> DeleteDirectHistory(int directId);
        Response<string> DeleteDirect(int directId);
    }
}
