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
    public class RelationService : IRelationService
    {
        private IUnitOfWork _unitOfWork;

        public RelationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Response<List<UserModel>> GetFollowers(int userId)
        {
            if (_unitOfWork.Users.Get(userId) == null) 
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.NotFound);

            List<int> followerIds = _unitOfWork.Followers.GetFollowers(userId).Select(f => f.FollowerId).ToList();
            List<UserModel> followers = new List<UserModel>();
            foreach (int id in followerIds)
            {
                var user = _unitOfWork.Users.Get(id);
                followers.Add(new UserModel() 
                {
                    Id = user.Id,
                    Username = user.Username,
                    Name = user.Name,
                    IsPublic = user.IsPublic
                });
            }

            return new Response<List<UserModel>>() 
            {
                Status = StatusCodes.Status200OK,
                Data = followers
            };
        }

        public Response<List<UserModel>> GetFollowers(string username)
        {
            User user = _unitOfWork.Users.GetByUsername(username);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.NotFound);

            return GetFollowers(user.Id);
        }

        public Response<List<UserModel>> GetFollowings(int userId)
        {
            if (_unitOfWork.Users.Get(userId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.NotFound);

            List<int> followingIds = _unitOfWork.Followers.Find(f => (f.FollowerId == userId) && f.Pending == 0)
                .Select(f => f.UserId).ToList();
            List<UserModel> followings = new List<UserModel>();
            foreach (int id in followingIds)
            {
                User user = _unitOfWork.Users.Get(id);
                followings.Add(new UserModel() 
                {
                    Id = user.Id,
                    Username = user.Username,
                    Name = user.Name,
                    IsPublic = user.IsPublic
                });
            }
            
            return new Response<List<UserModel>>() 
            {
                Status = StatusCodes.Status200OK,
                Data = followings
            };
        }

        public Response<List<UserModel>> GetFollowings(string username)
        {
            User user = _unitOfWork.Users.GetByUsername(username);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.NotFound);

            return GetFollowings(user.Id);
        }

        public Response<List<UserModel>> GetFollowRequests(int userId)
        {
            if (_unitOfWork.Users.Get(userId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.NotFound);

            List<int> followRequestsIds = _unitOfWork.Followers.Find(f => (f.UserId == userId) && f.Pending == 1)
                .Select(f => f.FollowerId).ToList();
            List<UserModel> followRequests = new List<UserModel>();
            foreach (int id in followRequestsIds)
            {
                User user = _unitOfWork.Users.Get(id);
                followRequests.Add(new UserModel() 
                {
                    Id = user.Id,
                    Username = user.Username,
                    Name = user.Name,
                    IsPublic = user.IsPublic
                });
            }

            return new Response<List<UserModel>>() 
            {
                Status = StatusCodes.Status200OK,
                Data = followRequests
            };
        }

        public Response<List<UserModel>> GetFollowRequests(string username)
        {
            User user = _unitOfWork.Users.GetByUsername(username);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.NotFound);

            return GetFollowRequests(user.Id);
        }

        public Response<string> SendFollowRequest(int userId, int followerId)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null || _unitOfWork.Users.Get(followerId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            if (_unitOfWork.Followers.HasFollower(userId, followerId))
                throw new HttpException(StatusCodes.Status409Conflict, Alerts.AlreadyFollowed);

            bool isPublicUser = user.IsPublic == 1;
            DirectMessage follower = new DirectMessage()
            {
                UserId = userId,
                FollowerId = followerId,
                Pending = (isPublicUser) ? 0 : 1
            };
            _unitOfWork.Followers.Add(follower);
            _unitOfWork.Save();

            return new Response<string>()
            {
                Status = StatusCodes.Status201Created,
                Data = Alerts.FollowRequestSent
            };
        }

        public Response<string> AcceptFollowRequest(int userId, int followerId)
        {
            if (_unitOfWork.Users.Get(userId) == null || _unitOfWork.Users.Get(followerId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            if (!_unitOfWork.Followers.HasRequestFrom(userId, followerId))
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.NoRequestFromThisUser);

            DirectMessage follower = _unitOfWork.Followers.Find(f => f.UserId == userId && f.FollowerId == followerId).First();
            follower.Pending = 0;
            _unitOfWork.Save();

            return new Response<string>()
            {
                Status = StatusCodes.Status200OK,
                Data = Alerts.RequestAccepted
            };
        }

        public Response<string> RejectFollowRequest(int userId, int followerId)
        {
            if (_unitOfWork.Users.Get(userId) == null || _unitOfWork.Users.Get(followerId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            if (!_unitOfWork.Followers.HasRequestFrom(userId, followerId))
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.NoRequestFromThisUser);

            DirectMessage follower = _unitOfWork.Followers.Find(f => f.UserId == userId && f.FollowerId == followerId).First();
            _unitOfWork.Followers.Remove(follower);
            _unitOfWork.Save();

            return new Response<string>()
            {
                Status = StatusCodes.Status200OK,
                Data = Alerts.RequestRejected
            };
        }

        public Response<string> CancelRequest(int userId, int followerId)
        {
            if (_unitOfWork.Users.Get(userId) == null || _unitOfWork.Users.Get(followerId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            DirectMessage request = _unitOfWork.Followers
                                .Find(f => f.UserId == userId && f.FollowerId == followerId && f.Pending == 1)
                                .FirstOrDefault();
            if (request == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.RequestNotFound);

            _unitOfWork.Followers.Remove(request);
            _unitOfWork.Save();

            return new Response<string>()
            {
                Status = StatusCodes.Status200OK,
                Data = Alerts.RequestCanceled
            };
        }

        public Response<string> Unfollow(int userId, int followerId)
        {
            if (_unitOfWork.Users.Get(userId) == null || _unitOfWork.Users.Get(followerId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            if (!_unitOfWork.Followers.HasFollower(userId, followerId))
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.RelationNotFound);

            DirectMessage follower = _unitOfWork.Followers.Find(f => f.UserId == userId && f.FollowerId == followerId).First();
            _unitOfWork.Followers.Remove(follower);
            _unitOfWork.Save();

            return new Response<string>()
            {
                Status = StatusCodes.Status200OK,
                Data = Alerts.UserUnfollowed
            };
        }
    }
}
