using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Interfaces
{
    public interface IRelationService
    {
        Response<List<UserModel>> GetFollowers(int id);
        Response<List<UserModel>> GetFollowers(string username);
        Response<List<UserModel>> GetFollowings(int id);
        Response<List<UserModel>> GetFollowings(string username);
        Response<List<UserModel>> GetFollowRequests(int id);
        Response<List<UserModel>> GetFollowRequests(string username);
        void SendFollowRequest(int userId, int followerId);
        void AcceptFollowRequest(int userId, int followerId);
        void RejectFollowRequest(int userId, int followerId);
        void CancelRequest(int userId, int followerId);
        void Unfollow(int userId, int followerId);

    }
}
