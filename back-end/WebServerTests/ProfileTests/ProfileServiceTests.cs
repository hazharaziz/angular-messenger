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
    public class ProfileServiceTests
    {
        private readonly ProfileService _profileService;
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();

        public ProfileServiceTests()
        {
            _profileService = new ProfileService(_unitOfWork.Object);
        }

        [Fact]
        public void GetProfile_ReturnsUserModelResponse()
        {
            // Arrange
            User user = Users.Adam;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Adam);
            UserModel expected = UserModels.Adam;

            // Act
            var response = _profileService.GetProfile(user.Id);

            // Assert
            Assert.Equal(expected.Id, response.Data.Id);
            Assert.True(expected.Username == response.Data.Username);
            Assert.InRange<int>(response.Data.IsPublic, 0, 1);
        }

        [Fact]
        public void GetProfile_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Adam;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _profileService.GetProfile(user.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void EditProfile_ReturnsStringResponse()
        {
            // Arrange
            UserModel user = UserModels.Adam;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Adam);
            _unitOfWork.Setup(u => u.Users.GetByUsername(It.IsAny<string>()))
                .Returns(Users.Null);
            _unitOfWork.Setup(u => u.Save());

            // Act
            var response = _profileService.EditProfile(user.Id, user);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void EditProfile_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            UserModel user = UserModels.Adam;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _profileService.EditProfile(user.Id, user);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void EditProfile_ThrowsHttpException_Status409Conflict()
        {
            // Arrange
            UserModel user = UserModels.Adam;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Patrick);
            _unitOfWork.Setup(u => u.Users.GetByUsername(It.IsAny<string>()))
                .Returns(Users.Patrick);

            // Act
            Action action = () => _profileService.EditProfile(Users.Patrick.Id, user);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void ChangePassword_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Isaac;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Save());

            // Act
            var response = _profileService.ChangePassword(user.Id, user.Password, "1234");

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void ChangePassword_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Isaac;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _profileService.ChangePassword(user.Id, user.Password, "1234");

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void ChangePassword_ThrowsHttpException_Status401Unauthorized()
        {
            // Arrange
            User user = Users.Adam;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(new User() 
                { 
                    Id = Users.Adam.Id,
                    Username = Users.Adam.Username,
                    Name = Users.Adam.Name,
                    Password = "1234",
                    IsPublic = Users.Adam.IsPublic
                });

            // Act
            Action action = () => _profileService.ChangePassword(user.Id, user.Password, "1234");

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteAccount_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Adam;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Adam);
            _unitOfWork.Setup(u => u.Users.Remove(It.IsAny<User>()));
            _unitOfWork.Setup(u => u.Save());

            // Act
            var response = _profileService.DeleteAccount(user.Id);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void DeleteAccount_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Patrick;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _profileService.DeleteAccount(user.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

    }
}
