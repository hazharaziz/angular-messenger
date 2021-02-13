using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.DBModels;

namespace WebServer.Interfaces
{
    public interface IRelationAPI
    {
        List<User> GetFollowers(int id);
        List<User> GetFollowers(string username);
        List<User> GetFollowings(int id);
        List<User> GetFollowings(string username);
        List<User> GetFollowRequests(int id);
        List<User> GetFollowRequests(string username);
        void SendFollowRequest(int userId, int followerId);
        void AcceptFollowRequest(int userId, int followerId);
        void RejectFollowRequest(int userId, int followerId);
        void CancelRequest(int userId, int followerId);
        void Unfollow(int userId, int followerId);
        bool HasFollower(int userId, int followerId);
        bool HasRequestFrom(int userId, int followerid);
    }
}
