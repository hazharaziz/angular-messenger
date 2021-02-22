using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Exceptions;
using WebServer.Interfaces;
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;
using WebServer.Services;
using WebServerTests.TestData;
using Xunit;

namespace WebServerTests
{
    public class GroupServiceTests
    {
        private readonly GroupService _groupService;
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();

        public GroupServiceTests()
        {
            _groupService = new GroupService(_unitOfWork.Object);
        }

        [Fact]
        public void GetUserGroups_ReturnsGroupModelsResponse()
        {
            // Arrange
            User user = Users.Isaac;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Groups.GetAll())
                .Returns(new List<Group>() { Groups.Group1, Groups.Group2, Groups.Group3 });
            _unitOfWork.SetupSequence(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true)
                .Returns(false)
                .Returns(true);
            List<GroupModel> expected = 
                new List<GroupModel>() {GroupModels.GroupModel1, GroupModels.GroupModel2 };

            // Act
            Response<List<GroupModel>> response = _groupService.GetUserGroups(user.Id);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.True(expected.Count == response.Data.Count);
            Assert.Equal(expected[0].GroupId, response.Data[0].GroupId);
            Assert.NotEmpty(response.Data);
        }

        [Fact]
        public void GetUserGroups_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Isaac;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _groupService.GetUserGroups(user.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GetGroupInfo_ReturnsGroupInfoModelResponse()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group2;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac)
                .Returns(Users.Patrick)
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group2);
            _unitOfWork.Setup(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
            _unitOfWork.Setup(u => u.GroupMembers.GetGroupMembers(It.IsAny<int>()))
                .Returns(new List<int>() 
                { 
                    GroupMembers.Record2.UserId, 
                    GroupMembers.Record6.UserId 
                });
            List<UserModel> expectedMembers = new List<UserModel>()
            {
                UserModels.Isaac,
                UserModels.Patrick
            };


            // Act
            Response<GroupInfoModel> response = 
                _groupService.GetGroupInfo(user.Id, group.GroupId);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.Equal(group.GroupId, response.Data.GroupId);
            Assert.Equal(group.MembersCount, response.Data.MembersCount);
            Assert.Equal(group.CreatorId, response.Data.CreatorId);
            Assert.Equal(expectedMembers.Count, response.Data.Members.Count);
            Assert.True(response.Data.Members.Exists(u => u.Id == UserModels.Isaac.Id));
        }

        [Fact]
        public void GetGroupInfo_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group2;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _groupService.GetGroupInfo(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GetGroupInfo_ThrowsHttpException_Status404NotFound_GroupNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group2;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Null);

            // Act
            Action action = () => _groupService.GetGroupInfo(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GetGroupInfo_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group3;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac)
                .Returns(Users.Patrick)
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group3);
            _unitOfWork.Setup(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);

            // Act
            Action action = () => _groupService.GetGroupInfo(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GetAvailableFriends_ReturnsUserModelsResponse()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group2;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group2);
            _unitOfWork.Setup(u => u.Users.GetAll())
                .Returns(new List<User>() 
                { Users.Adam, Users.Isaac, Users.Nico, Users.Oscar, Users.Patrick });
            _unitOfWork.SetupSequence(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false).Returns(true).Returns(false).Returns(false).Returns(true);
            _unitOfWork.SetupSequence(u => u.Followers.IsFriend(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true).Returns(false).Returns(false);

            // Act
            Response<List<UserModel>> response = 
                _groupService.GetAvailableFriends(user.Id, group.GroupId);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.NotEmpty(response.Data);
            Assert.Equal(Users.Adam.Id, response.Data[0].Id);
        }

        [Fact]
        public void GetAvailableFriends_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group2;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _groupService.GetAvailableFriends(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GetAvailableFriends_ThrowsHttpException_Status404NotFound_GroupNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group2;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Null);

            // Act
            Action action = () => _groupService.GetAvailableFriends(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void CreateGroup_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Add(It.IsAny<Group>()));
            _unitOfWork.Setup(u => u.Save());

            // Act
            Response<string> response = _groupService.CreateGroup(user.Id, group);

            // Assert
            Assert.Equal(StatusCodes.Status201Created, response.Status);
        }

        [Fact]
        public void CreateGroup_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _groupService.CreateGroup(user.Id, group);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void CreateGroup_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group2;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);

            // Act
            Action action = () => _groupService.CreateGroup(user.Id, group);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void EditGroup_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group1;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group1);
            _unitOfWork.Setup(u => u.Save());

            // Act
            var response = _groupService.EditGroup(user.Id, group.GroupId, group);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void EditGroup_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group1;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _groupService.EditGroup(user.Id, group.GroupId, group);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void EditGroup_ThrowsHttpException_Status404NotFound_GroupNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group1;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Null);

            // Act
            Action action = () => _groupService.EditGroup(user.Id, group.GroupId, group);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void EditGroup_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group2;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group2);

            // Act
            Action action = () => _groupService.EditGroup(user.Id, group.GroupId, group);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteGroup_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group1;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group1);
            _unitOfWork.Setup(u => u.Groups.Remove(It.IsAny<Group>()));
            _unitOfWork.Setup(u => u.Save());

            // Act
            var response = _groupService.DeleteGroup(user.Id, group.GroupId);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void DeleteGroup_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group1;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _groupService.DeleteGroup(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteGroup_ThrowsHttpException_Status404NotFound_GroupNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group1;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Null);

            // Act
            Action action = () => _groupService.DeleteGroup(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteGroup_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group2;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group2);

            // Act
            Action action = () => _groupService.DeleteGroup(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void AddMembersToGroup_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group1;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac)
                .Returns(Users.Adam);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group1);
            _unitOfWork.SetupSequence(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true)
                .Returns(false);
            _unitOfWork.SetupSequence(u => u.Followers.IsFriend(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            // Act
            var response = _groupService.AddMembersToGroup
                (user.Id, group.GroupId, new List<int>() { Users.Adam.Id });

            // Assert
            Assert.Equal(StatusCodes.Status201Created, response.Status);
        }

        [Fact]
        public void AddMembersToGroup_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group1;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = 
                () => _groupService.AddMembersToGroup
                     (user.Id, group.GroupId, new List<int>() { Users.Adam.Id });

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void AddMembersToGroup_ThrowsHttpException_Status404NotFound_GroupNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group1;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Null);


            // Act
            Action action =
                () => _groupService.AddMembersToGroup
                     (user.Id, group.GroupId, new List<int>() { Users.Adam.Id });

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void AddMembersToGroup_ThrowsHttpException_Status405MethodNotAllowed_UserNotGroupMember()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group1;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group1);
            _unitOfWork.SetupSequence(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);

            // Act
            Action action =
                () => _groupService.AddMembersToGroup
                     (user.Id, group.GroupId, new List<int>() { Users.Adam.Id });

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void AddMembersToGroup_ThrowsHttpException_Status405MethodNotAllowed_NotAdmin()
        {
            // Arrange
            User user = Users.Adam;
            Group group = Groups.Group1;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Adam)
                .Returns(Users.Nico);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group1);
            _unitOfWork.SetupSequence(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true)
                .Returns(false);
            _unitOfWork.SetupSequence(u => u.Followers.IsFriend(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            // Act
            Action action =
                () => _groupService.AddMembersToGroup
                     (user.Id, group.GroupId, new List<int>() { Users.Nico.Id });

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void AddMembersToGroup_ThrowsHttpException_Status404NotFound_MemberNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group1;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac)
                .Returns(Users.Null);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group1);
            _unitOfWork.SetupSequence(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            // Act
            Action action =
                () => _groupService.AddMembersToGroup
                     (user.Id, group.GroupId, new List<int>() { Users.Adam.Id });

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void AddMembersToGroup_ThrowsHttpException_Status400BadRequest()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group1;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac)
                .Returns(Users.Adam);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group1);
            _unitOfWork.SetupSequence(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true)
                .Returns(true);

            // Act
            Action action =
                () => _groupService.AddMembersToGroup
                     (user.Id, group.GroupId, new List<int>() { Users.Adam.Id });

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void AddMembersToGroup_ThrowsHttpException_Status405MethodNotAllowed_NotFriends()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group1;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac)
                .Returns(Users.Nico);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group1);
            _unitOfWork.SetupSequence(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true)
                .Returns(false);
            _unitOfWork.SetupSequence(u => u.Followers.IsFriend(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);

            // Act
            Action action =
                () => _groupService.AddMembersToGroup
                     (user.Id, group.GroupId, new List<int>() { Users.Nico.Id });

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void RemoveMemberFromGroup_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Patrick;
            Group group = Groups.Group2;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Patrick);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group2);
            _unitOfWork.Setup(u => u.GroupMembers.GetGroupMember(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(GroupMembers.Record6);
            _unitOfWork.Setup(u => u.GroupMembers.Remove(It.IsAny<GroupMember>()));
            _unitOfWork.Setup(u => u.Save());

            // Act
            var response = _groupService
                .RemoveMemberFromGroup(user.Id, group.GroupId, Users.Isaac.Id);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void RemoveMemberFromGroup_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Patrick;
            Group group = Groups.Group2;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action =
                () => _groupService.RemoveMemberFromGroup(user.Id, group.GroupId, Users.Isaac.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void RemoveMemberFromGroup_ThrowsHttpException_Status404NotFound_GroupNotFound()
        {
            // Arrange
            User user = Users.Patrick;
            Group group = Groups.Group2;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Patrick);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Null);

            // Act
            Action action =
                () => _groupService.RemoveMemberFromGroup(user.Id, group.GroupId, Users.Isaac.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void RemoveMemberFromGroup_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group2;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group2);

            // Act
            Action action =
                () => _groupService.RemoveMemberFromGroup(user.Id, group.GroupId, Users.Isaac.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void RemoveMemberFromGroup_ThrowsHttpException_Status404NotFound_MemberNotFound()
        {
            // Arrange
            User user = Users.Patrick;
            Group group = Groups.Group2;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Patrick);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group2);
            _unitOfWork.Setup(u => u.GroupMembers.GetGroupMember(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(GroupMembers.Null);

            // Act
            Action action =
                () => _groupService.RemoveMemberFromGroup(user.Id, group.GroupId, Users.Nico.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GetGroupMessages_ReturnsGroupMessagesResponse()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group3);
            _unitOfWork.Setup(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
            _unitOfWork.Setup(u => u.GroupMessages.GetGroupMessages(It.IsAny<int>()))
                .Returns(new List<GroupMessage>()
                {
                    GroupMessages.GroupMessage7,
                    GroupMessages.GroupMessage8
                });

            List<GroupMessage> expectedMessages = new List<GroupMessage>()
            {
                GroupMessages.GroupMessage8,
                GroupMessages.GroupMessage7
            };

            // Act
            var response = _groupService.GetGroupMessages(user.Id, group.GroupId);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.Equal(expectedMessages.Count, response.Data.Count);
            Assert.Equal
                (expectedMessages[0].GroupMessageId, response.Data[0].GroupMessageId);
            Assert.NotEmpty(response.Data);
        }

        [Fact]
        public void GetGroupMessages_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _groupService.GetGroupMessages(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GetGroupMessages_ThrowsHttpException_Status404NotFound_GroupNotFound()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Null);

            // Act
            Action action = () => _groupService.GetGroupMessages(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GetGroupMessages_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            User user = Users.Nico;
            Group group = Groups.Group3;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group3);
            _unitOfWork.Setup(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);

            // Act
            Action action = () => _groupService.GetGroupMessages(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void SendGroupMessage_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage7;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group3);
            _unitOfWork.Setup(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
            _unitOfWork.Setup(u => u.GroupMessages.Add(It.IsAny<GroupMessage>()));
            _unitOfWork.Setup(u => u.Save());

            // Act
            var response = _groupService.SendGroupMessage(user.Id, group.GroupId, message);

            // Assert
            Assert.Equal(StatusCodes.Status201Created, response.Status);
        }

        [Fact]
        public void SendGroupMessage_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage7;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = 
                () => _groupService.SendGroupMessage(user.Id, group.GroupId, message);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void SendGroupMessage_ThrowsHttpException_Status404NotFound_GroupNotFound()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage7;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Null);

            // Act
            Action action =
                () => _groupService.SendGroupMessage(user.Id, group.GroupId, message);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void SendGroupMessage_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            User user = Users.Nico;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage7;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group3);
            _unitOfWork.Setup(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);

            // Act
            Action action =
                () => _groupService.SendGroupMessage(user.Id, group.GroupId, message);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void EditGroupMessage_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage8;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group3);
            _unitOfWork.Setup(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
            _unitOfWork.Setup(u => u.GroupMessages.Get(It.IsAny<int>()))
                .Returns(GroupMessages.GroupMessage8);
            _unitOfWork.Setup(u => u.Save());

            // Act
            var response = _groupService.EditGroupMessage(user.Id, group.GroupId, message);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void EditGroupMessage_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage8;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action =
                () => _groupService.EditGroupMessage(user.Id, group.GroupId, message);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void EditGroupMessage_ThrowsHttpException_Status404NotFound_GroupNotFound()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage8;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Null);

            // Act
            Action action =
                () => _groupService.EditGroupMessage(user.Id, group.GroupId, message);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void EditGroupMessage_ThrowsHttpException_Status405MethodNotAllowed_NotGroupMember()
        {
            // Arrange
            User user = Users.Nico;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage8;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group3);
            _unitOfWork.Setup(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            // Act
            Action action =
                () => _groupService.EditGroupMessage(user.Id, group.GroupId, message);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void EditGroupMessage_ThrowsHttpException_Status404NotFound_MessageNotFound()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage8;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group3);
            _unitOfWork.Setup(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
            _unitOfWork.Setup(u => u.GroupMessages.Get(It.IsAny<int>()))
                .Returns(GroupMessages.Null);

            // Act
            Action action =
                () => _groupService.EditGroupMessage(user.Id, group.GroupId, message);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void EditGroupMessage_ThrowsHttpException_Status405MethodNotAllowed_NotMessageComposer()
        {
            // Arrange
            User user = Users.Patrick;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage8;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Patrick);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group3);
            _unitOfWork.Setup(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
            _unitOfWork.Setup(u => u.GroupMessages.Get(It.IsAny<int>()))
                .Returns(GroupMessages.GroupMessage8);

            // Act
            Action action =
                () => _groupService.EditGroupMessage(user.Id, group.GroupId, message);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteGroupMessage_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage8;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group3);
            _unitOfWork.Setup(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
            _unitOfWork.Setup(u => u.GroupMessages.Get(It.IsAny<int>()))
                .Returns(GroupMessages.GroupMessage8);
            _unitOfWork.Setup(u => u.GroupMessages.Remove(It.IsAny<GroupMessage>()));
            _unitOfWork.Setup(u => u.Save());

            // Act
            var response = _groupService.DeleteGroupMessage
                (user.Id, group.GroupId, message.GroupMessageId);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void DeleteGroupMessage_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage8;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = 
                () => _groupService.DeleteGroupMessage
                (user.Id, group.GroupId, message.GroupMessageId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteGroupMessage_ThrowsHttpException_Status404NotFound_GroupNotFound()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage8;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Null);

            // Act
            Action action =
                () => _groupService.DeleteGroupMessage
                (user.Id, group.GroupId, message.GroupMessageId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteGroupMessage_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage8;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group3);
            _unitOfWork.Setup(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);

            // Act
            Action action =
                () => _groupService.DeleteGroupMessage
                (user.Id, group.GroupId, message.GroupMessageId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteGroupMessage_ThrowsHttpException_Status404NotFound_MessageNotFound()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage8;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group3);
            _unitOfWork.Setup(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
            _unitOfWork.Setup(u => u.GroupMessages.Get(It.IsAny<int>()))
                .Returns(GroupMessages.Null);

            // Act
            Action action =
                () => _groupService.DeleteGroupMessage
                (user.Id, group.GroupId, message.GroupMessageId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteGroupMessage_ThrowsHttpException_Status405MethodNotAllowed_NotMessageComposer()
        {
            // Arrange
            User user = Users.Patrick;
            Group group = Groups.Group2;
            GroupMessage message = GroupMessages.GroupMessage5;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Patrick);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group2);
            _unitOfWork.Setup(u => u.GroupMembers.IsMemberOfGroup(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
            _unitOfWork.Setup(u => u.GroupMessages.Get(It.IsAny<int>()))
                .Returns(GroupMessages.GroupMessage5);

            // Act
            Action action =
                () => _groupService.DeleteGroupMessage
                (user.Id, group.GroupId, message.GroupMessageId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void ClearGroupHistory_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage8;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group3);
            _unitOfWork.Setup(u => u.GroupMessages.GetGroupMessages(It.IsAny<int>()))
                .Returns(new List<GroupMessage>()
                {
                    GroupMessages.GroupMessage7,
                    GroupMessages.GroupMessage8
                });
            _unitOfWork.Setup(u => u.GroupMessages.Remove(It.IsAny<GroupMessage>()));
            _unitOfWork.Setup(u => u.Save());

            // Act
            var response = _groupService.ClearGroupHistory(user.Id, group.GroupId);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void ClearGroupHistory_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage8;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _groupService.ClearGroupHistory(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void ClearGroupHistory_ThrowsHttpException_Status404NotFound_GroupNotFound()
        {
            // Arrange
            User user = Users.Oscar;
            Group group = Groups.Group3;
            GroupMessage message = GroupMessages.GroupMessage8;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Null);

            // Act
            Action action = () => _groupService.ClearGroupHistory(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void LeaveGroup_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group2;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group2);
            _unitOfWork.Setup(u => u.GroupMembers.GetGroupMember(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(GroupMembers.Record6);

            // Act
            var response = _groupService.LeaveGroup(user.Id, group.GroupId);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void LeaveGroup_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group2;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _groupService.LeaveGroup(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void LeaveGroup_ThrowsHttpException_Status404NotFound_GroupNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group2;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Null);

            // Act
            Action action = () => _groupService.LeaveGroup(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void LeaveGroup_ThrowsHttpException_Status404NotFound_MemberNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Group group = Groups.Group2;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Groups.Get(It.IsAny<int>()))
                .Returns(Groups.Group2);
            _unitOfWork.Setup(u => u.GroupMembers.GetGroupMember(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(GroupMembers.Null);

            // Act
            Action action = () => _groupService.LeaveGroup(user.Id, group.GroupId);

            // Assert
            Assert.Throws<HttpException>(action);
        }
    }
}
