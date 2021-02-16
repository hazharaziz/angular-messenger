﻿using Microsoft.AspNetCore.Http;
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
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.NotFound);

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

        public Response<UserModel> EditProfile(int userId, UserModel editedUser)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.NotFound);

            user.Username = editedUser.Username;
            user.Name = editedUser.Name;
            user.IsPublic = editedUser.IsPublic;
            _unitOfWork.Save();

            return new Response<UserModel>()
            {
                Status = StatusCodes.Status200OK,
                Data = editedUser
            };
        }

        public Response<string> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.NotFound);

            if (user.Password != oldPassword)
                throw new HttpException(StatusCodes.Status401Unauthorized, Alerts.WrongPassword);

            return new Response<string>
            {
                Status = StatusCodes.Status200OK,
                Data = Alerts.PasswordChanged
            };
        }
    }
}