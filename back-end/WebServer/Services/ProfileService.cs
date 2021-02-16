using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Interfaces;
using WebServer.Models.ResponseModels;

namespace WebServer.Services
{
    public class ProfileService : IProfileService
    {
        private IUnitOfWork _unitOfWork;

        public ProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Response<UserModel> GetProfile(int userId)
        {
            throw new NotImplementedException();
        }

        public Response<UserModel> EditProfile(int userId, UserModel editedUser)
        {
            throw new NotImplementedException();
        }

        public Response<string> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
