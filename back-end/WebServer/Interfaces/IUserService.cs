using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.ResponseModels;

namespace WebServer.Interfaces
{
    public interface IUserService
    {
        Response<List<UserModel>> GetAllUsers(int userId);
        Response<List<UserModel>> FilterUsers(int userId, string text);
        Response<List<UserModel>> GetUserFriends(int userId);
    }
}
