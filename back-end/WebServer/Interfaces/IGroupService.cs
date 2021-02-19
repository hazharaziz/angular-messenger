using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Interfaces
{
    public interface IGroupService
    {
        Response<List<GroupModel>> GetUserGroups(int userId);
        Response<Group> GetGroupInfo(int groupId);
        Response<string> CreateGroup(Group group);
        Response<string> EditGroup(int userId, Group editedGroup);
        Response<string> DeleteGroup(int userId, int groupId);
        Response<string> AddMembersToGroup(int groupId, List<int> memberIds);
        Response<string> RemoveMemberFromGroup(int groupId, int memberId);
        Response<string> SendGroupMessage(int userId, int groupId, GroupMessage message);
        Response<string> EditMessage(int userId, int groupId, GroupMessage editedMessage);
        Response<string> DeleteMessage(int userId, int messageId);
        Response<string> ClearGroupHistory(int userId, int groupId);
        Response<string> LeaveGroup(int userId, int groupId);
    }
}
