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
    public class GroupService : IGroupService
    {
        private IUnitOfWork _unitOfWork;

        public GroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Response<List<GroupModel>> GetUserGroups(int userId) 
        {
            if (_unitOfWork.Users.Get(userId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            List<GroupModel> groups = new List<GroupModel>();
            _unitOfWork.Groups.GetAll().ForEach(group =>
            {
                if (_unitOfWork.GroupMembers.IsMemberOfGroup(userId, group.GroupId))
                    groups.Add(new GroupModel() { GroupId = group.GroupId, GroupName = group.GroupName });
            });

            return new Response<List<GroupModel>>() { Status = StatusCodes.Status200OK, Data = groups };
        }

        public Response<Group> GetGroupInfo(int userId, int groupId)
        {
            if (_unitOfWork.Users.Get(userId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            if (!_unitOfWork.GroupMembers.IsMemberOfGroup(userId, groupId))
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            Group group = _unitOfWork.Groups.GetGroupInfo(groupId);

            return new Response<Group>() { Status = StatusCodes.Status200OK, Data = group };
        }

        public Response<string> CreateGroup(int userId, Group group)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            if (user.Id != group.CreatorId)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            group.MembersCount = 1;
            _unitOfWork.Groups.Add(group);
            _unitOfWork.Save();

            return new Response<string>() { Status = StatusCodes.Status201Created, Data = Alerts.GroupCreated };
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
