using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Exceptions;
using WebServer.Interfaces;
using WebServer.Messages;
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;
using WebServer.Services;
using WebServerTests.TestData;
using Xunit;

namespace WebServerTests
{
    public class DirectServiceTests
    {
        private readonly DirectService _directService;
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();

        public DirectServiceTests()
        {
            _directService = new DirectService(_unitOfWork.Object);
        }

        [Fact]
        public void GetUserDirects_ReturnsDirectsResponse()
        {
            // Arrange
            User user = Users.Adam;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Adam)
                .Returns(Users.Null)
                .Returns(Users.Nico)
                .Returns(Users.Isaac);
            List<Direct> directs = new List<Direct>()
            {
                Directs.Direct1,
                Directs.Direct3,
                Directs.Direct4
            };
            _unitOfWork.Setup(u => u.Directs.GetDirects(It.IsAny<int>()))
                .Returns(directs);  

            List<DirectModel> expected = new List<DirectModel>()
            {
                new DirectModel() { Id = directs[0].DirectId, DirectName = "Deleted Account" },
                new DirectModel() { Id = directs[1].DirectId, DirectName = Users.Nico.Name },
                new DirectModel() { Id = directs[2].DirectId, DirectName = Users.Isaac.Name },
            };


            // Act
            var response = _directService.GetUserDirects(user.Id);

            // Assert
            Assert.IsType<Response<List<DirectModel>>>(response);
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.Equal(expected.Count, response.Data.Count);
            Assert.NotEqual(Users.Patrick.Name, response.Data[0].DirectName);
        }

        [Fact]
        public void GetUserDirects_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Adam;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action = () => _directService.GetUserDirects(user.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GetDirectMessages_ReturnsDirectMesssagesResponse()
        {
            // Arrange
            User user = Users.Patrick;
            Direct direct = Directs.Direct2;
            _unitOfWork.Setup(u => u.Directs.Get(It.IsAny<int>()))
                .Returns(Directs.Direct2);
            _unitOfWork.Setup(u => u.DirectMessages.GetDirectMessages(It.IsAny<int>()))
                .Returns(new List<DirectMessage>()
                {
                    DirectMessages.DirectMessage2,
                    DirectMessages.DirectMessage3,
                    DirectMessages.DirectMessage4
                });

            List<DirectMessage> expected = new List<DirectMessage>()
            {
                DirectMessages.DirectMessage4,
                DirectMessages.DirectMessage3,
                DirectMessages.DirectMessage2
            };

            // Act
            Response<List<DirectMessage>> response =
                _directService.GetDirectMessages(user.Id, direct.DirectId);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.Equal(expected.Count, response.Data.Count);
            Assert.NotEqual(expected[2].DirectMessageId, response.Data[0].DirectMessageId);
            Assert.Equal(expected[1], response.Data[1]);
        }

        [Fact]
        public void GetDirectMessages_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Patrick;
            Direct direct = Directs.Direct2;
            _unitOfWork.Setup(u => u.Directs.Get(It.IsAny<int>()))
                .Returns(Directs.Null);

            // Act
            Action action = () => _directService.GetDirectMessages(user.Id, direct.DirectId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GetDirectMessages_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            User user = Users.Adam;
            Direct direct = Directs.Direct2;
            _unitOfWork.Setup(u => u.Directs.Get(It.IsAny<int>()))
                .Returns(Directs.Direct2);

            // Act
            Action action = () => _directService.GetDirectMessages(user.Id, direct.DirectId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void SendDirectMessage_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Oscar;
            User targetUser = Users.Isaac;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar)
                .Returns(Users.Isaac);

            _unitOfWork.Setup(u => u.Directs.Get(It.IsAny<int>()))
                .Returns(Directs.Null);
            _unitOfWork.Setup(u => u.Directs.Add(It.IsAny<Direct>()));
            _unitOfWork.Setup(u => u.Directs.Get(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Directs.Direct5);
            _unitOfWork.Setup(u => u.DirectMessages.Add(It.IsAny<DirectMessage>()));
            _unitOfWork.Setup(u => u.Save());

            // Act
            Response<string> response =
                _directService.SendDirectMessage(user.Id, targetUser.Id, DirectMessages.DirectMessage5);

            // Assert
            Assert.Equal(StatusCodes.Status201Created, response.Status);
            Assert.Equal(Alerts.MessageCreated, response.Data);
        }

        [Fact]
        public void SendDirectMessage_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Oscar;
            User targetUser = Users.Isaac;
            _unitOfWork.SetupSequence(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null)
                .Returns(targetUser);

            // Act
            Action action = 
                () => _directService.SendDirectMessage
                (user.Id, targetUser.Id, DirectMessages.DirectMessage5);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void EditDirectMessage_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Isaac;
            DirectMessage message = DirectMessages.DirectMessage5;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);

            _unitOfWork.Setup(u => u.DirectMessages.Get(It.IsAny<int>()))
                .Returns(DirectMessages.DirectMessage5);
            _unitOfWork.Setup(u => u.Save());

            // Act
            Response<string> response =
                _directService.EditDirectMessage(user.Id, message.DirectMessageId, message);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.Equal(Alerts.MessageEdited, response.Data);
        }

        [Fact]
        public void EditDirectMessage_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            DirectMessage message = DirectMessages.DirectMessage5;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action =
                () => _directService.EditDirectMessage
                (user.Id, message.DirectMessageId, message);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void EditDirectMessage_ThrowsHttpException_Status404NotFound_MessageNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            DirectMessage message = DirectMessages.DirectMessage5;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.DirectMessages.Get(It.IsAny<int>()))
                .Returns(DirectMessages.Null);

            // Act
            Action action =
                () => _directService.EditDirectMessage
                (user.Id, message.DirectMessageId, message);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void EditDirectMessage_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            User user = Users.Isaac;
            DirectMessage message = DirectMessages.DirectMessage4;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.DirectMessages.Get(It.IsAny<int>()))
                .Returns(DirectMessages.DirectMessage4);

            // Act
            Action action =
                () => _directService.EditDirectMessage
                (user.Id, message.DirectMessageId, message);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteDirectMessage_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Isaac;
            DirectMessage message = DirectMessages.DirectMessage5;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);

            _unitOfWork.Setup(u => u.DirectMessages.Get(It.IsAny<int>()))
                .Returns(DirectMessages.DirectMessage5);
            _unitOfWork.Setup(u => u.Save());

            // Act
            Response<string> response =
                _directService.DeleteDirectMessage(user.Id, message.DirectMessageId);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.Equal(Alerts.MessageDeleted, response.Data);
        }

        [Fact]
        public void DeleteDirectMessage_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            DirectMessage message = DirectMessages.DirectMessage5;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action =
                () => _directService.DeleteDirectMessage
                (user.Id, message.DirectMessageId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteDirectMessage_ThrowsHttpException_Status404NotFound_MessageNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            DirectMessage message = DirectMessages.DirectMessage5;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.DirectMessages.Get(It.IsAny<int>()))
                .Returns(DirectMessages.Null);

            // Act
            Action action =
                () => _directService.DeleteDirectMessage
                (user.Id, message.DirectMessageId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteDirectMessage_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            User user = Users.Isaac;
            DirectMessage message = DirectMessages.DirectMessage4;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.DirectMessages.Get(It.IsAny<int>()))
                .Returns(DirectMessages.DirectMessage4);

            // Act
            Action action =
                () => _directService.DeleteDirectMessage
                (user.Id, message.DirectMessageId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteDirect_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Isaac;
            Direct direct = Directs.Direct7;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);

            _unitOfWork.Setup(u => u.Directs.Get(It.IsAny<int>()))
                .Returns(Directs.Direct7);
            _unitOfWork.Setup(u => u.Directs.Remove(It.IsAny<Direct>()));
            _unitOfWork.Setup(u => u.Save());

            // Act
            Response<string> response = _directService.DeleteDirect(user.Id, direct.DirectId);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.Equal(Alerts.DirectDeleted, response.Data);
        }

        [Fact]
        public void DeleteDirect_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Direct direct = Directs.Direct7;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action =
                () => _directService.DeleteDirect
                (user.Id, direct.DirectId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteDirect_ThrowsHttpException_Status404NotFound_DirectNotFound()
        {
            // Arrange
            User user = Users.Isaac;
            Direct direct = Directs.Direct7;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);

            _unitOfWork.Setup(u => u.Directs.Get(It.IsAny<int>()))
                .Returns(Directs.Null);

            // Act
            Action action =
                () => _directService.DeleteDirect
                (user.Id, direct.DirectId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteDirect_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            User user = Users.Isaac;
            Direct direct = Directs.Direct6;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Directs.Get(It.IsAny<int>()))
                .Returns(Directs.Direct6);

            // Act
            Action action =
                () => _directService.DeleteDirect
                (user.Id, direct.DirectId);

            // Assert
            Assert.Throws<HttpException>(action);
        }


        [Fact]
        public void DeleteDirectHistory_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Oscar;
            Direct direct = Directs.Direct5;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);

            _unitOfWork.Setup(u => u.Directs.Get(It.IsAny<int>()))
                .Returns(Directs.Direct5);
            _unitOfWork.Setup(u => u.DirectMessages.GetAll())
                .Returns(new List<DirectMessage>() 
                {
                    DirectMessages.DirectMessage5,
                    DirectMessages.DirectMessage6
                });
            _unitOfWork.Setup(u => u.Save());

            // Act
            Response<string> response = 
                _directService.DeleteDirectHistory(user.Id, direct.DirectId);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.Equal(Alerts.HistoryDeleted, response.Data);
        }

        [Fact]
        public void DeleteDirectHistory_ThrowsHttpException_Status404NotFound_UserNotFound()
        {
            // Arrange
            User user = Users.Oscar;
            Direct direct = Directs.Direct5;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Null);

            // Act
            Action action =
                () => _directService.DeleteDirectHistory
                (user.Id, direct.DirectId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteDirectHistory_ThrowsHttpException_Status404NotFound_DirectNotFound()
        {
            // Arrange
            User user = Users.Oscar;
            Direct direct = Directs.Direct5;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);

            _unitOfWork.Setup(u => u.Directs.Get(It.IsAny<int>()))
                .Returns(Directs.Null);

            // Act
            Action action =
                () => _directService.DeleteDirectHistory
                (user.Id, direct.DirectId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteDirectHistory_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            User user = Users.Oscar;
            Direct direct = Directs.Direct4;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                .Returns(Users.Oscar);

            _unitOfWork.Setup(u => u.Directs.Get(It.IsAny<int>()))
                .Returns(Directs.Direct4);

            // Act
            Action action =
                () => _directService.DeleteDirectHistory
                (user.Id, direct.DirectId);

            // Assert
            Assert.Throws<HttpException>(action);
        }
    }
}
