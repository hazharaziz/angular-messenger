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
    public class ChatService : IChatService
    {
        private IUnitOfWork _unitOfWork;
        private IRelationService _relationService;

        public ChatService(IUnitOfWork unitOfWork, IRelationService relationService)
        {
            _unitOfWork = unitOfWork;
            _relationService = relationService;
        }

        public Response<List<Message>> FetchFriendsMessages(int userId)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            List<int> followingsIds = _unitOfWork.Followers.GetFollowings(userId)
                                                 .Select(f => f.UserId)
                                                 .ToList();
            List<Message> allMessages = _unitOfWork.Messages.GetAll()
                        .Where(message => followingsIds.Contains(message.ComposerId) || 
                               message.ComposerId == userId)
                        .OrderByDescending(m => m.DateTime)
                        .ToList();

            return new Response<List<Message>>() 
            { 
                Status = StatusCodes.Status200OK,
                Data =  allMessages
            };
        }

        public Response<string> AddMessage(int userId, Message message)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            message.ComposerId = user.Id;
            message.ComposerName = user.Name;
            _unitOfWork.Messages.Add(message);
            _unitOfWork.Save();
            return new Response<string>()
            {
                Status = StatusCodes.Status201Created,
                Data = Alerts.MessageCreated
            };
        }

        public Response<string> EditMessage(int userId, int messageId, Message message)
        {
            Message targetMessage = _unitOfWork.Messages.Get(messageId);
            if (targetMessage == null) 
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.MessageNotFound);

            if (targetMessage.ComposerId != userId)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            targetMessage.Text = message.Text;
            _unitOfWork.Save();

            return new Response<string>()
            {
                Status = StatusCodes.Status200OK,
                Data = Alerts.MessageEdited
            };
        }

        public Response<string> DeleteMessage(int userId, int messageId)
        {
            Message message = _unitOfWork.Messages.Get(messageId);
            if (message == null) 
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.MessageNotFound);

            if (message.ComposerId != userId)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            _unitOfWork.Messages.Remove(message);
            _unitOfWork.Save();

            return new Response<string>()
            {
                Status = StatusCodes.Status200OK,
                Data = Alerts.MessageDeleted
            };
        }
    }
}
