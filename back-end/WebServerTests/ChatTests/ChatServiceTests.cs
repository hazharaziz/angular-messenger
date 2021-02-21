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
    public class ChatServiceTests
    {
        private readonly ChatService _chatService;
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IRelationService> _relationSevice = new Mock<IRelationService>();

        public ChatServiceTests()
        {
            _chatService = new ChatService(_unitOfWork.Object, _relationSevice.Object);
        }

        [Fact]
        public void FetchFriendsMessages_ReturnsMessagesResponse()
        {
            // Arrange
            User user = Users.Nico;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                       .Returns(Users.Nico);

            List<Follower> relations = new List<Follower>()
            {
                Relations.Relation5,
                Relations.Relation9
            };
            List<Message> messages = new List<Message>
            {
                Messages.OscarMessage1,
                Messages.OscarMessage2,
                Messages.AdamMessage1,
                Messages.AdamMessage2,
                Messages.PatricMessage1
            };
            _unitOfWork.Setup(u => u.Followers.GetFollowings(It.IsAny<int>()))
                       .Returns(relations);
            _unitOfWork.Setup(u => u.Messages.GetAll())
                       .Returns(messages);

            List<Message> expected = messages
                .Where(m => m.ComposerId != Users.Patrick.Id)
                .OrderByDescending(m => m.DateTime)
                .ToList();

            // Act
            Response<List<Message>> response = _chatService.FetchFriendsMessages(user.Id);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
            Assert.Equal(expected, response.Data);
        }

        [Fact]
        public void FetchFriendsMessages_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Patrick;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>())).Returns(Users.Null);

            // Act
            Action action = () => _chatService.FetchFriendsMessages(user.Id);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void AddMessage_ReturnsStringResponse()
        {
            // Arrange
            User user = Users.Isaac;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>())).Returns(Users.Isaac);
            _unitOfWork.Setup(u => u.Messages.Add(It.IsAny<Message>()));
            _unitOfWork.Setup(u => u.Save());

            // Act
            Response<string> response = _chatService.AddMessage(user.Id, Messages.IsaacMessage1);

            // Assert
            Assert.Equal(StatusCodes.Status201Created, response.Status);
        }

        [Fact]
        public void AddMessage_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            User user = Users.Nico;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>())).Returns(Users.Null);

            // Act
            Action action = () => _chatService.AddMessage(user.Id, Messages.NicoMessage1);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void EditMessage_ReturnsStringResponse()
        {
            // Arrange
            Message message = Messages.OscarMessage3;
            User user = Users.Oscar;
            _unitOfWork.Setup(u => u.Messages.Get(It.IsAny<int>()))
                .Returns(Messages.OscarMessage3);
            _unitOfWork.Setup(u => u.Save());

            // Act
            Response<string> response = _chatService
                .EditMessage(user.Id, message.MessageId, message);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void EditMessage_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            Message message = Messages.OscarMessage3;
            User user = Users.Oscar;
            _unitOfWork.Setup(u => u.Messages.Get(It.IsAny<int>()))
                .Returns(Messages.Null);

            // Act
            Action action = () => _chatService
                .EditMessage(user.Id, message.MessageId, message);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void EditMessage_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            Message message = Messages.AdamMessage1;
            User user = Users.Oscar;
            _unitOfWork.Setup(u => u.Messages.Get(It.IsAny<int>()))
                .Returns(Messages.AdamMessage1);

            // Act
            Action action = () => _chatService
                .EditMessage(user.Id, message.MessageId, message);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteMessage_ReturnsStringResponse()
        {
            // Arrange
            Message message = Messages.PatricMessage1;
            User user = Users.Patrick;
            _unitOfWork.Setup(u => u.Messages.Get(It.IsAny<int>()))
                .Returns(Messages.PatricMessage1);
            _unitOfWork.Setup(u => u.Messages.Remove(It.IsAny<Message>()));
            _unitOfWork.Setup(u => u.Save());

            // Act
            Response<string> response = _chatService
                .DeleteMessage(user.Id, message.MessageId);

            // Assert
            Assert.Equal(StatusCodes.Status200OK, response.Status);
        }

        [Fact]
        public void DeleteMessage_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            Message message = Messages.OscarMessage3;
            User user = Users.Oscar;
            _unitOfWork.Setup(u => u.Messages.Get(It.IsAny<int>()))
                .Returns(Messages.Null);

            // Act
            Action action = () => _chatService
                .DeleteMessage(user.Id, message.MessageId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void DeleteMessage_ThrowsHttpException_Status405MethodNotAllowed()
        {
            // Arrange
            Message message = Messages.AdamMessage1;
            User user = Users.Oscar;
            _unitOfWork.Setup(u => u.Messages.Get(It.IsAny<int>()))
                .Returns(Messages.AdamMessage1);

            // Act
            Action action = () => _chatService
                .DeleteMessage(user.Id, message.MessageId);

            // Assert
            Assert.Throws<HttpException>(action);
        }

    }
}
