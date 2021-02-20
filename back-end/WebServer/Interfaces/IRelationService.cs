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
        Response<string> SendFollowRequest(int userId, int followerId);
        Response<string> AcceptFollowRequest(int userId, int followerId);
        Response<string> RejectFollowRequest(int userId, int followerId);
        Response<string> CancelRequest(int userId, int followerId);
        Response<string> DeleteRelation(int userId, int followerId);
    }
}
