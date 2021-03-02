using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Exceptions;
using WebServer.Interfaces;
using WebServer.Messages;
using WebServer.Models.DBModels;
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
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            return new Response<UserModel>()
            {
                Status = StatusCodes.Status200OK,
                Data = new UserModel()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Name = user.Name,
                    IsPublic = user.IsPublic
                }
            };
        }

        public Response<string> EditProfile(int userId, UserModel editedUser)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);
            
            if (editedUser.Username != user.Username)
                if (_unitOfWork.Users.GetByUsername(editedUser.Username) != null)
                    throw new HttpException(StatusCodes.Status409Conflict, Alerts.UsernameExists);

            user.Username = editedUser.Username;
            user.Name = editedUser.Name;
            user.IsPublic = editedUser.IsPublic;
            _unitOfWork.Save();

            return new Response<string>() { Status = StatusCodes.Status200OK, Data = Alerts.ProfileEdited };
        }

        public Response<string> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            if (user.Password != oldPassword)
                throw new HttpException(StatusCodes.Status401Unauthorized, Alerts.WrongAuthenticationCredentials);

            user.Password = newPassword;
            _unitOfWork.Save();

            return new Response<string>
            {
                Status = StatusCodes.Status200OK,
                Data = Alerts.PasswordChanged
            };
        }

        public Response<string> DeleteAccount(int userId)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            _unitOfWork.Users.Remove(user);
            _unitOfWork.Save();

            return new Response<string>()
            {
                Status = StatusCodes.Status200OK,
                Data = Alerts.AccountDeleted
            };
        }
    }
}
