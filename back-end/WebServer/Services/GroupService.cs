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
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            List<GroupModel> groups = new List<GroupModel>();
            _unitOfWork.Groups.GetAll().ForEach(group =>
            {
                if (_unitOfWork.GroupMembers.IsMemberOfGroup(userId, group.GroupId))
                    groups.Add(new GroupModel() 
                    { 
                        GroupId = group.GroupId, 
                        GroupName = group.GroupName,
                        CreatorId = group.CreatorId,
                        CreatorName = group.CreatorName
                    });
            });

            return new Response<List<GroupModel>>() { Status = StatusCodes.Status200OK, Data = groups };
        }

        public Response<GroupInfoModel> GetGroupInfo(int userId, int groupId)
        {
            if (_unitOfWork.Users.Get(userId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            Group group = _unitOfWork.Groups.Get(groupId);
            if (group == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.GroupNotFound);

            if (!_unitOfWork.GroupMembers.IsMemberOfGroup(userId, groupId))
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotGroupMember);


            List<UserModel> members = new List<UserModel>();
            _unitOfWork.GroupMembers.GetGroupMembers(groupId).ForEach(memberId =>
            {
                UserModel userModel = new UserModel();
                User member = _unitOfWork.Users.Get(memberId);
                if (member == null)
                {
                    userModel.Id = memberId;
                    userModel.Name = "Deleted Account";
                }
                else
                {
                    userModel = new UserModel()
                    {
                        Id = member.Id,
                        Username = member.Username,
                        Name = member.Name,
                        IsPublic = member.IsPublic
                    };
                }
                members.Add(userModel);
            });

            GroupInfoModel groupInfo = new GroupInfoModel()
            {
                GroupId = group.GroupId,
                GroupName = group.GroupName,
                CreatorId = group.CreatorId,
                CreatorName = group.CreatorName,
                AddMemberAccess = group.AddMemberAccess,
                MembersCount = group.MembersCount,
                Members = members
            };

            return new Response<GroupInfoModel>() { Status = StatusCodes.Status200OK, Data = groupInfo };
        }

        public Response<List<UserModel>> GetAvailableFriends(int userId, int groupId)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            Group group = _unitOfWork.Groups.Get(groupId);
            if (group == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.GroupNotFound);

            List<UserModel> friends = new List<UserModel>();
            _unitOfWork.Users.GetAll().ForEach(record =>
            {
                if (!_unitOfWork.GroupMembers.IsMemberOfGroup(record.Id, groupId))
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

        public Response<string> CreateGroup(int userId, Group group)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            if (user.Id != group.CreatorId)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            group.CreatorName = user.Name;
            group.MembersCount = 1;
            _unitOfWork.Groups.Add(group);
            _unitOfWork.Save();

            return new Response<string>() { Status = StatusCodes.Status201Created, Data = Alerts.GroupCreated };
        }

        public Response<string> EditGroup(int userId, int groupId, Group editedGroup)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            Group group = _unitOfWork.Groups.Get(groupId);
            if (group == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.GroupNotFound);

            if (group.CreatorId != userId)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            group.GroupName = editedGroup.GroupName;
            group.AddMemberAccess = editedGroup.AddMemberAccess;
            _unitOfWork.Save();

            return new Response<string>() { Status = StatusCodes.Status200OK, Data = Alerts.GroupEdited };
        }

        public Response<string> DeleteGroup(int userId, int groupId)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            Group group = _unitOfWork.Groups.Get(groupId);
            if (group == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.GroupNotFound);

            if (userId != group.CreatorId)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            _unitOfWork.Groups.Remove(group);
            _unitOfWork.Save();

            return new Response<string>() { Status = StatusCodes.Status200OK, Data = Alerts.GroupDeleted };
        }

        public Response<string> AddMembersToGroup(int userId, int groupId, List<int> memberIds)
        {
            if (_unitOfWork.Users.Get(userId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            Group group = _unitOfWork.Groups.Get(groupId);
            if (group == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.GroupNotFound);

            if (!_unitOfWork.GroupMembers.IsMemberOfGroup(userId, groupId))
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotGroupMember);

            if (group.AddMemberAccess == 0)
            {
                if (userId != group.CreatorId)
                    throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);
            }

            List<GroupMember> members = new List<GroupMember>();
            foreach (int memberId in memberIds)
            {
                if (memberId == userId)
                    continue;
                User user = _unitOfWork.Users.Get(memberId);
                if (user == null)
                    throw new HttpException(StatusCodes.Status404NotFound, Alerts.MemberNotFound);

                if (_unitOfWork.GroupMembers.IsMemberOfGroup(memberId, groupId))
                    throw new HttpException(StatusCodes.Status400BadRequest, Alerts.AlreadyMember);

                if (!_unitOfWork.Followers.IsFriend(userId, memberId))
                    throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotFriends);

                members.Add(new GroupMember() { UserId = memberId, GroupId = groupId });
                group.MembersCount++;
            }

            _unitOfWork.GroupMembers.AddRange(members);
            _unitOfWork.Save();
            return new Response<string>() { Status = StatusCodes.Status201Created, Data = Alerts.MemberAdded };
        }

        public Response<string> RemoveMemberFromGroup(int userId, int groupId, int memberId)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            Group group = _unitOfWork.Groups.Get(groupId);
            if (group == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.GroupNotFound);

            if (userId != group.CreatorId || userId == memberId)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            GroupMember groupMember = _unitOfWork.GroupMembers.GetGroupMember(groupId, memberId);
            if (groupMember == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.MemberNotFound);

            group.MembersCount--;
            _unitOfWork.GroupMembers.Remove(groupMember);
            _unitOfWork.Save();

            return new Response<string>() { Status = StatusCodes.Status200OK, Data = Alerts.MemberRemoved };
        }

        public Response<List<GroupMessage>> GetGroupMessages(int userId, int groupId)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            Group group = _unitOfWork.Groups.Get(groupId);
            if (group == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.GroupNotFound);

            if (!_unitOfWork.GroupMembers.IsMemberOfGroup(userId, groupId))
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotGroupMember);

            List<GroupMessage> groupMessages = _unitOfWork.GroupMessages
                    .GetGroupMessages(groupId).OrderByDescending(m => m.DateTime).ToList();
            return new Response<List<GroupMessage>>() { Status = StatusCodes.Status200OK, Data = groupMessages };
        }

        public Response<string> SendGroupMessage(int userId, int groupId, GroupMessage message)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            Group group = _unitOfWork.Groups.Get(groupId);
            if (group == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.GroupNotFound);

            if (!_unitOfWork.GroupMembers.IsMemberOfGroup(userId, groupId))
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotGroupMember);

            message.ComposerId = user.Id;
            message.ComposerName = user.Name;
            message.GroupId = groupId;
            _unitOfWork.GroupMessages.Add(message);
            _unitOfWork.Save();

            return new Response<string>() { Status = StatusCodes.Status201Created, Data = Alerts.MessageCreated };
        }

        public Response<string> EditGroupMessage(int userId, int groupId, int messageId, GroupMessage editedMessage)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            Group group = _unitOfWork.Groups.Get(groupId);
            if (group == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.GroupNotFound);

            if (!_unitOfWork.GroupMembers.IsMemberOfGroup(userId, groupId))
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotGroupMember);

            GroupMessage message = _unitOfWork.GroupMessages.Get(messageId);

            if (message == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.MessageNotFound);

            if (message.ComposerId != user.Id)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            message.Text = editedMessage.Text;
            _unitOfWork.Save();

            return new Response<string>() { Status = StatusCodes.Status200OK, Data = Alerts.MessageEdited };            
        }

        public Response<string> DeleteGroupMessage(int userId, int groupId, int messageId)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            Group group = _unitOfWork.Groups.Get(groupId);
            if (group == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            if (!_unitOfWork.GroupMembers.IsMemberOfGroup(userId, groupId))
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotGroupMember);

            GroupMessage message = _unitOfWork.GroupMessages.Get(messageId);
            if (message == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.MessageNotFound);

            if (message.ComposerId != user.Id)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            _unitOfWork.GroupMessages.Remove(message);
            _unitOfWork.Save();

            return new Response<string>() { Status = StatusCodes.Status200OK, Data = Alerts.MessageDeleted };
        }

        public Response<string> ClearGroupHistory(int userId, int groupId)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            Group group = _unitOfWork.Groups.Get(groupId);
            if (group == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.GroupNotFound);

            _unitOfWork.GroupMessages.GetGroupMessages(groupId).ForEach(message =>
            {
                _unitOfWork.GroupMessages.Remove(message);
            });
            _unitOfWork.Save();

            return new Response<string>() { Status = StatusCodes.Status200OK, Data = Alerts.HistoryDeleted };
        }

        public Response<string> LeaveGroup(int userId, int groupId)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            Group group = _unitOfWork.Groups.Get(groupId);
            if (group == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.GroupNotFound);

            GroupMember record = _unitOfWork.GroupMembers.GetGroupMember(groupId, userId);
            if (record == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.MemberNotFound);

            _unitOfWork.GroupMembers.Remove(record);
            _unitOfWork.Save();

            return new Response<string>() { Status = StatusCodes.Status200OK, Data = Alerts.LeftGroup };
        }
    }
}
