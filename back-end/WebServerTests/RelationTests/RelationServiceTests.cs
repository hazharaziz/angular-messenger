using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class RelationServiceTests
    {
        private readonly RelationService _relationService;
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        
        public RelationServiceTests()
        {
            _relationService = new RelationService(_unitOfWork.Object);
        }

        [Fact]
        public void GetFollowers_ReturnsUserModelsResponse()
        {
            // Arrange
            User user = Users.Nico;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico)
                .Returns(Users.Adam);
            _unitOfWork.Setup(u => u.Followers.GetFollowers(It.IsAny<int>()))
                .Returns(new List<Follower>() { Relations.Relation3 });
            List<UserModel> expected = new List<UserModel>() { UserModels.Adam };

            // Act
            var response = _relationService.GetFollowers(user.Id);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.Equal(expected.Count, response.Data.Count);
            Assert.Equal(expected[0].Username, response.Data[0].Username);
            Assert.Equal(expected[0].Name, response.Data[0].Name);
        }

        [Fact]
        public void GetFollowers_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Nico;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _relationService.GetFollowers(user.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GetFollowings_ReturnsUserModelsResponse()
        {
            // Arrange
            User user = Users.Nico;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico)
                .Returns(Users.Adam)
                .Returns(Users.Oscar);
            _unitOfWork.Setup(u => u.Followers.GetFollowings(It.IsAny<int>()))
                .Returns(new List<Follower>()
                {
                    Relations.Relation5,
                    Relations.Relation9,
                });
            List<UserModel> expected = new List<UserModel>()
            {
                UserModels.Adam,
                UserModels.Oscar
            };

            // Act
            var response = _relationService.GetFollowings(user.Id);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.Equal(expected.Count, response.Data.Count);
            Assert.Equal(expected[0].Username, response.Data[0].Username);
            Assert.Equal(expected[1].Id, response.Data[1].Id);
        }

        [Fact]
        public void GetFollowings_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Nico;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _relationService.GetFollowings(user.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GetFollowRequests_ReturnsUserModelsResponse()
        {
            // Arrange
            User user = Users.Nico;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico)
                .Returns(Users.Patrick);
            _unitOfWork.Setup(u => u.Followers.GetReceivedRequests(It.IsAny<int>()))
                .Returns(new List<Follower>() { Relations.Relation8 });
            List<UserModel> expected = new List<UserModel>() { UserModels.Patrick };

            // Act
            var response = _relationService.GetReceivedRequests(user.Id);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.Equal(expected.Count, response.Data.Count);
            Assert.Equal(expected[0].Username, response.Data[0].Username);
            Assert.Equal(expected[0].Id, response.Data[0].Id);
        }

        [Fact]
        public void GetFollowRequests_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Nico;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _relationService.GetReceivedRequests(user.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void SendFollowRequest_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Adam;
            User follower = Users.Isaac;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Adam)
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Followers.HasFollower(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            _unitOfWork.Setup(u => u.Followers.HasRequestFrom(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            _unitOfWork.Setup(u => u.Followers.Add(It.IsAny<Follower>()));
            _unitOfWork.Setup(u => u.Save());

            // Act
            var response = _relationService.SendFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Equal(StatusCodes.Status201Created, response.Status);
        }

        [Fact]
        public void SendFollowRequest_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Adam;
            User follower = Users.Isaac;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Adam)
                .Returns(Users.Null);

            // Act
            Action action = () => _relationService.SendFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void SendFollowRequest_ThrowsHttpException_Status409Conflict_AlreadyFollowed()
        {
            // Arrange
            User user = Users.Adam;
            User follower = Users.Isaac;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Adam)
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Followers.HasFollower(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            // Act
            Action action = () => _relationService.SendFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void SendFollowRequest_ThrowsHttpException_Status409Conflict_AlreadySentRequest()
        {
            // Arrange
            User user = Users.Adam;
            User follower = Users.Isaac;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Adam)
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Followers.HasFollower(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            _unitOfWork.Setup(u => u.Followers.HasRequestFrom(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            // Act
            Action action = () => _relationService.SendFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void SendFollowRequest_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            User user = Users.Adam;
            User follower = Users.Adam;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Adam)
                .Returns(Users.Adam);
            _unitOfWork.Setup(u => u.Followers.HasFollower(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            _unitOfWork.Setup(u => u.Followers.HasRequestFrom(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);

            // Act
            Action action = () => _relationService.SendFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void AcceptFollowRequest_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Nico;
            User follower = Users.Patrick;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico)
                .Returns(Users.Patrick);
            _unitOfWork.Setup(u => u.Followers.HasFollower(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            _unitOfWork.Setup(u => u.Followers.HasRequestFrom(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
            _unitOfWork.Setup(u => u.Followers.GetRequest(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Relations.Relation8);
            _unitOfWork.Setup(u => u.Save());

            // Act
            var response = _relationService.AcceptFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void AcceptFollowRequest_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Nico;
            User follower = Users.Patrick;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico)
                .Returns(Users.Null);

            // Act
            Action action = () => _relationService.AcceptFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void AcceptFollowRequest_ThrowsHttpException_Status400BadRequest()
        {
            // Arrange
            User user = Users.Nico;
            User follower = Users.Patrick;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico)
                .Returns(Users.Patrick);
            _unitOfWork.Setup(u => u.Followers.HasFollower(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            // Act
            Action action = () => _relationService.AcceptFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void AcceptFollowRequest_ThrowsHttpException_Status404NotFound_NoRequestFromThisUser()
        {
            // Arrange
            User user = Users.Nico;
            User follower = Users.Patrick;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico)
                .Returns(Users.Patrick);
            _unitOfWork.Setup(u => u.Followers.HasFollower(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            _unitOfWork.Setup(u => u.Followers.HasRequestFrom(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);

            // Act
            Action action = () => _relationService.AcceptFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void RejectFollowRequest_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Nico;
            User follower = Users.Patrick;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico)
                .Returns(Users.Patrick);
            _unitOfWork.Setup(u => u.Followers.HasFollower(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            _unitOfWork.Setup(u => u.Followers.HasRequestFrom(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
            _unitOfWork.Setup(u => u.Followers.GetRequest(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Relations.Relation8);
            _unitOfWork.Setup(u => u.Followers.Remove(It.IsAny<Follower>()));
            _unitOfWork.Setup(u => u.Save());

            // Act
            var response = _relationService.RejectFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void RejectFollowRequest_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Nico;
            User follower = Users.Patrick;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico)
                .Returns(Users.Null);

            // Act
            Action action = () => _relationService.RejectFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void RejectFollowRequest_ThrowsHttpException_Status400BadRequest()
        {
            // Arrange
            User user = Users.Nico;
            User follower = Users.Patrick;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico)
                .Returns(Users.Patrick);
            _unitOfWork.Setup(u => u.Followers.HasFollower(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            // Act
            Action action = () => _relationService.RejectFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void RejectFollowRequest_ThrowsHttpException_Status404NotFound_NoRequestFromThisUser()
        {
            // Arrange
            User user = Users.Nico;
            User follower = Users.Patrick;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico)
                .Returns(Users.Patrick);
            _unitOfWork.Setup(u => u.Followers.HasFollower(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            _unitOfWork.Setup(u => u.Followers.HasRequestFrom(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);

            // Act
            Action action = () => _relationService.RejectFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void CancelFollowRequest_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Nico;
            User follower = Users.Patrick;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico)
                .Returns(Users.Patrick);
            _unitOfWork.Setup(u => u.Followers.HasFollower(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);
            _unitOfWork.Setup(u => u.Followers.HasRequestFrom(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
            _unitOfWork.Setup(u => u.Followers.GetRequest(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Relations.Relation8);
            _unitOfWork.Setup(u => u.Followers.Remove(It.IsAny<Follower>()));
            _unitOfWork.Setup(u => u.Save());

            // Act
            var response = _relationService.CancelFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void CancelFollowRequest_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Nico;
            User follower = Users.Patrick;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico)
                .Returns(Users.Null);

            // Act
            Action action = () => _relationService.CancelFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void CancelFollowRequest_ThrowsHttpException_Status400BadRequest()
        {
            // Arrange
            User user = Users.Nico;
            User follower = Users.Patrick;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Nico)
                .Returns(Users.Patrick);
            _unitOfWork.Setup(u => u.Followers.HasFollower(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);

            // Act
            Action action = () => _relationService.CancelFollowRequest(user.Id, follower.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteRelation_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Adam;
            User follower = Users.Isaac;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Adam)
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Followers.HasFollower(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true);
            _unitOfWork.Setup(u => u.Followers.GetRelation(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Relations.Relation7);
            _unitOfWork.Setup(u => u.Followers.Remove(It.IsAny<Follower>()));
            _unitOfWork.Setup(u => u.Save());

            // Act
            var response = _relationService.DeleteRelation(user.Id, follower.Id);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void DeleteRelation_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Adam;
            User follower = Users.Isaac;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null)
                .Returns(Users.Isaac);

            // Act
            Action action = () => _relationService.DeleteRelation(user.Id, follower.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteRelation_ThrowsHttpException_Status404NotFound_RelationNotFound()
        {
            // Arrange
            User user = Users.Adam;
            User follower = Users.Isaac;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Adam)
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Followers.HasFollower(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false);

            // Act
            Action action = () => _relationService.DeleteRelation(user.Id, follower.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }
    }
}
