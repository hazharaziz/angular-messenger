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

        public List<Message> FetchMessages()
            => _unitOfWork.Messages.GetAll().ToList();

        public Response<List<Message>> FetchFriendsMessages(int userId)
        {
            List<int> followingsIds = _relationService.GetFollowings(userId).Data.Select(f => f.Id).ToList();
            List<Message> allMessages = FetchMessages();

            return new Response<List<Message>>() 
            { 
                Status = StatusCodes.Status200OK,
                Data = allMessages.Where(message =>
                        followingsIds.Contains(message.ComposerId) || message.ComposerId == userId)
                        .OrderByDescending(m => m.DateTime).ToList()
            };             

        }

        public Response<List<Message>> FetchFriendsMessages(string username)
        {
            User user = _unitOfWork.Users.Find(u => u.Username == username).FirstOrDefault();
            if (user == null)
            {
                throw new Exception();
            }
            return FetchFriendsMessages(user.Id);
        }

        public Response<string> AddMessage(Message message)
        {
            _unitOfWork.Messages.Add(message);
            _unitOfWork.Save();
            return new Response<string>()
            {
                Status = StatusCodes.Status201Created,
                Data = Alerts.MessageCreatedSuccess
            };
        }

        public Response<string> EditMessage(int id, Message message)
        {
            Message targetMessage = _unitOfWork.Messages.Get(id);
            if (targetMessage == null) throw new HttpException(StatusCodes.Status404NotFound, Alerts.NotFound);

            targetMessage.Text = message.Text;
            targetMessage.ReplyToId = message.ReplyToId;

            _unitOfWork.Save();

            return new Response<string>()
            {
                Status = StatusCodes.Status200OK,
                Data = Alerts.MessageEditedSuccess
            };
        }

        public Response<string> DeleteMessage(int id)
        {
            Message message = _unitOfWork.Messages.Get(id);
            if (message == null) throw new HttpException(StatusCodes.Status404NotFound, Alerts.NotFound);

            _unitOfWork.Messages.Remove(message);
            _unitOfWork.Save();

            return new Response<string>()
            {
                Status = StatusCodes.Status200OK,
                Data = Alerts.MessageEditedSuccess
            };
        }
    }
}
