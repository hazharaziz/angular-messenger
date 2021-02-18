using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Interfaces
{
    public interface IDirectInterface
    {
        Response<List<DirectModel>> GetUserDirects(int userId);
        Response<List<DirectMessage>> GetDirectMessages(int directId);
    }
}
