using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Interfaces;
using WebServer.Models.DBModels;

namespace WebServer.Services
{
    public class Relations : IRelationAPI
    {
        private IUnitOfWork _unitOfWork;

        public Relations(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<User> GetFollowers(int userId)
        {
            List<int> followerIds = _unitOfWork.Followers.GetFollowers(userId).Select(f => f.FollowerId).ToList();
            List<User> followers = new List<User>();
            foreach (int id in followerIds)
            {
                var user = _unitOfWork.Users.Get(id);
                followers.Add(user);
            }
            return followers;
        }

        public List<User> GetFollowers(string username)
        {
            User user = _unitOfWork.Users.GetByUsername(username);
            return GetFollowers(user.Id);
        }

        public List<User> GetFollowings(int userId)
        {
            List<int> followingIds = _unitOfWork.Followers.Find(f => (f.FollowerId == userId) && f.Pending == 0)
                .Select(f => f.UserId).ToList();
            List<User> followings = new List<User>();
            foreach (int id in followingIds)
            {
                followings.Add(_unitOfWork.Users.Get(id));
            }
            return followings;
        }

        public List<User> GetFollowings(string username)
        {
            User user = _unitOfWork.Users.GetByUsername(username);
            return GetFollowings(user.Id);
        }

        public List<User> GetFollowRequests(int userId)
        {
            List<int> followRequestsIds = _unitOfWork.Followers.Find(f => (f.UserId == userId) && f.Pending == 1)
                .Select(f => f.FollowerId).ToList();
            List<User> followRequests = new List<User>();
            foreach (int id in followRequestsIds)
            {
                followRequests.Add(_unitOfWork.Users.Get(id));
            }
            return followRequests;
        }

        public List<User> GetFollowRequests(string username)
        {
            User user = _unitOfWork.Users.GetByUsername(username);
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
