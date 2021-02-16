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

        public void SendFollowRequest(int userId, int followerId)
        {
            bool isPublicUser = _unitOfWork.Users.Get(userId).IsPublic == 1;
            if (!_unitOfWork.Followers.HasFollower(userId, followerId))
            {
                Follower follower = new Follower()
                {
                    UserId = userId,
                    FollowerId = followerId,
                    Pending = (isPublicUser) ? 0 : 1
                };
                _unitOfWork.Followers.Add(follower);
                _unitOfWork.Save();
            }
        }

        public void AcceptFollowRequest(int userId, int followerId)
        {
            if (_unitOfWork.Followers.HasRequestFrom(userId, followerId))
            {
                Follower follower = _unitOfWork.Followers.Find(f => f.UserId == userId && f.FollowerId == followerId).First();
                follower.Pending = 0;
                _unitOfWork.Save();
            }
        }

        public void RejectFollowRequest(int userId, int followerId)
        {
            if (_unitOfWork.Followers.HasRequestFrom(userId, followerId))
            {
                Follower follower = _unitOfWork.Followers.Find(f => f.UserId == userId && f.FollowerId == followerId).First();
                _unitOfWork.Followers.Remove(follower);
                _unitOfWork.Save();
            }
        }

        public void CancelRequest(int userId, int followerId)
        {
            Follower follower = _unitOfWork.Followers.Find(f => f.UserId == userId && f.FollowerId == followerId).First();
            _unitOfWork.Followers.Remove(follower);
            _unitOfWork.Save();
        }

        public void Unfollow(int userId, int followerId)
        {
            if (_unitOfWork.Followers.HasFollower(userId, followerId))
            {
                Follower follower = _unitOfWork.Followers.Find(f => f.UserId == userId && f.FollowerId == followerId).First();
                _unitOfWork.Followers.Remove(follower);
                _unitOfWork.Save();
            }
        }
    }
}
