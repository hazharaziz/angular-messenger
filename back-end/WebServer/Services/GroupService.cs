using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Interfaces;
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Services
{
    public class GroupService : IGroupService
    {
        private IUnitOfWork _unitOfWork;

        public GroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Response<List<GroupModel>> GetUserGroups(int userId) 
        {
            throw new NotImplementedException();
        }

        public Response<Group> GetGroupInfo(int groupId)
        {
            throw new NotImplementedException();
        }

        public Response<string> CreateGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public Response<string> EditGroup(int userId, Group editedGroup)
        {
            throw new NotImplementedException();
        }

        public Response<string> DeleteGroup(int userId, int groupId)
        {
            throw new NotImplementedException();
        }

        public Response<string> AddMembersToGroup(int groupId, List<int> memberIds)
        {
            throw new NotImplementedException();
        }

        public Response<string> RemoveMemberFromGroup(int groupId, int memberId)
        {
            throw new NotImplementedException();
        }

        public Response<List<GroupMessage>> GetGroupMessages(int userId, int groupId)
        {
            throw new NotImplementedException();
        }

        public Response<string> SendGroupMessage(int userId, int groupId, GroupMessage message)
        {
            throw new NotImplementedException();
        }

        public Response<string> EditMessage(int userId, int groupId, GroupMessage editedMessage)
        {
            throw new NotImplementedException();
        }

        public Response<string> DeleteMessage(int userId, int messageId)
        {
            throw new NotImplementedException();
        }

        public Response<string> ClearGroupHistory(int userId, int groupId)
        {
            throw new NotImplementedException();
        }

        public Response<string> LeaveGroup(int userId, int groupId)
        {
            throw new NotImplementedException();
        }
    }
}
