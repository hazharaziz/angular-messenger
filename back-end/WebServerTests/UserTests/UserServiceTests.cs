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
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();

        public UserServiceTests()
        {
            _userService = new UserService(_unitOfWork.Object);
        }

        [Fact]
        public void GetAllUsers_ReturnsUserModelsResponse()
        {
            // Arrange
            User user = Users.Adam;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Adam);
            _unitOfWork.Setup(u => u.Users.GetAll())
                .Returns(new List<User>()
                {
                    Users.Adam,
                    Users.Patrick,
                    Users.Nico,
                    Users.Isaac,
                    Users.Oscar
                });
            List<UserModel> expected = new List<UserModel>()
            {
                UserModels.Adam,
                UserModels.Patrick,
                UserModels.Nico,
                UserModels.Isaac,
                UserModels.Oscar,
            };

            // Act
            var response = _userService.GetAllUsers(user.Id);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.Equal(expected.Count, response.Data.Count);
            Assert.Equal(expected[2].Name, response.Data[2].Name);
            Assert.Equal(expected[4].Id, response.Data[4].Id);
        }

        [Fact]
        public void GetAllUsers_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Adam;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _userService.GetAllUsers(user.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void FilterUsers_ReturnsUserModelsResponse()
        {
            // Arrange
            User user = Users.Adam;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Adam);
            _unitOfWork.Setup(u => u.Users.GetAll())
                .Returns(new List<User>()
                {
                    Users.Adam,
                    Users.Patrick,
                    Users.Nico
                });
            _unitOfWork.SetupSequence(u => u.Followers.HasFollower(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(true)
                .Returns(false)
                .Returns(false);
            List<UserModel> expected = new List<UserModel>()
            {
                UserModels.Patrick,
                UserModels.Nico,
            };

            // Act
            var response = _userService.FilterUsers(user.Id, "ic");

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.Equal(expected.Count, response.Data.Count);
            Assert.Equal(expected[1].Name, response.Data[1].Name);
            Assert.DoesNotContain(response.Data, u => u.Username == user.Username);
        }

        [Fact]
        public void FilterUsers_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Adam;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _userService.FilterUsers(user.Id, "ic");

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GetUserFriends_ReturnsUserModelsResponse()
        {
            // Arrange
            User user = Users.Adam;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Adam);
            _unitOfWork.Setup(u => u.Users.GetAll())
                .Returns(new List<User>()
                {
                    Users.Adam,
                    Users.Patrick,
                    Users.Nico,
                    Users.Isaac,
                    Users.Oscar
                });
            _unitOfWork.SetupSequence(u => u.Followers.IsFriend(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(false).Returns(true).Returns(true).Returns(false);
            List<UserModel> expected = new List<UserModel>()
            {
                UserModels.Nico,
                UserModels.Isaac
            };

            // Act
            var response = _userService.GetUserFriends(user.Id);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.Equal(expected.Count, response.Data.Count);
            Assert.Equal(expected[1].Name, response.Data[1].Name);
            Assert.Equal(expected[0].Username, response.Data[0].Username);
        }

        [Fact]
        public void GetUserFriends_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Adam;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _userService.GetUserFriends(user.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }
    }
}
