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
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Response<List<UserModel>> GetAllUsers(int userId)
        {
            if (_unitOfWork.Users.Get(userId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            List<UserModel> users = new List<UserModel>();
            _unitOfWork.Users.GetAll().ForEach(user =>
            {
                users.Add(new UserModel()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Name = user.Name,
                    IsPublic = user.IsPublic
                });
            });

            return new Response<List<UserModel>>() { Status = StatusCodes.Status200OK, Data = users };
        }

        public Response<List<UserModel>> FilterUsers(int userId, string text)
        {
            if (_unitOfWork.Users.Get(userId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            List<UserModel> filteredUsers = new List<UserModel>();
            if (text != "")
            {
                _unitOfWork.Users.GetAll().ForEach(user =>
                {
                    if (!_unitOfWork.Followers.HasFollower(userId, user.Id) &&
                        !_unitOfWork.Followers.HasFollower(user.Id, userId))
                    {
                        if ((user.Username.ToLower().Contains(text.ToLower()) ||
                            user.Name.ToLower().Contains(text.ToLower())) && user.Id != userId)
                        {
                            filteredUsers.Add(new UserModel()
                            {
                                Id = user.Id,
                                Username = user.Username,
                                Name = user.Name,
                                IsPublic = user.IsPublic
                            });
                        }
                    }
                });
            }

            return new Response<List<UserModel>>()
            {
                Status = StatusCodes.Status200OK,
                Data = filteredUsers
            };
        }

        public Response<List<UserModel>> GetUserFriends(int userId)
        {
            if (_unitOfWork.Users.Get(userId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            List<UserModel> friends = new List<UserModel>();
            _unitOfWork.Users.GetAll().ForEach(record =>
            {
                if (record.Id != userId)
                {
                    if (_unitOfWork.Followers.IsFriend(userId, record.Id))
                    {
                        friends.Add(new UserModel()
                        {
                            Id = record.Id,
                            Username = record.Username,
                            Name = record.Name,
                            IsPublic = record.IsPublic
                        });
                    }
                }
            });

            return new Response<List<UserModel>>()
            {
                Status = StatusCodes.Status200OK,
                Data = friends
            };
        }
    }
}
