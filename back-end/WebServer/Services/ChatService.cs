using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Interfaces;
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
            List<int> followingsIds = _relationService.GetFollowings(userId).Select(f => f.Id).ToList();
            List<Message> allMessages = FetchMessages();

            return new Response<List<Message>>() 
            { 
                Status = StatusCodes.Status200OK,
                Data = allMessages.Where(message =>
                        followingsIds.Contains(message.ComposerId) || message.ComposerId == userId)
                        .OrderByDescending(m => m.DateTime).ToList()
            };             
            //List<Message> filteredMessages = new List<Message>();
            //foreach (var message in allMessages)
            //{
            //    if (followingsIds.Contains(message.ComposerId) || message.ComposerId == userId)
            //    {
            //        filteredMessages.Add(message);
            //    }
            //}
            //return filteredMessages.OrderByDescending(m => m.DateTime).ToList();
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

        public void AddMessage(Message message)
        {
            if (message == null)
            {
                throw new Exception();
            }
            _unitOfWork.Messages.Add(message);
            _unitOfWork.Save();
        }

        public void EditMessage(int id, Message message)
        {
            if (message == null)
            {
                throw new Exception();
            }
            Message targetMessage = _unitOfWork.Messages.Get(id);
            targetMessage.Text = message.Text;
            targetMessage.ReplyToId = message.ReplyToId;
            _unitOfWork.Save();
        }

        public void DeleteMessage(int id)
        {
            Message message = _unitOfWork.Messages.Find(m => m.MessageId == id).FirstOrDefault();
            if (message == null)
            {
                throw new Exception();
            }
            _unitOfWork.Messages.Remove(message);
            _unitOfWork.Save();
        }

        public User GetCurrentUser(string username)
            => _unitOfWork.Users.GetByUsername(username);

    }
}
