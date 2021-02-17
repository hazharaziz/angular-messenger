using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Interfaces
{
    public interface IProfileService
    {
        public Response<UserModel> GetProfile(int userId);
        public Response<UserModel> EditProfile(int userId, UserModel editedUser);
        public Response<string> ChangePassword(int userId, string oldPassword, string newPassword);
        public Response<string> DeleteAccount(int userId);
    }
}
