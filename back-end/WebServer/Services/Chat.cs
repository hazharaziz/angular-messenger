using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Interfaces;
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Services
{
    public class Chat : IChatAPI
    {
        private IUnitOfWork _unitOfWork;
        private IRelationAPI _relationAPI;

        public Chat(IUnitOfWork unitOfWork, IRelationAPI followerAPI)
        {
            _unitOfWork = unitOfWork;
            _relationAPI = followerAPI;
        }

        public List<Message> FetchMessages()
            => _unitOfWork.Messages.GetAll().ToList();

        public List<ResponseMessage> FetchFriendsMessages(int userId)
        {
            List<int> followingsIds = _relationAPI.GetFollowings(userId).Select(f => f.Id).ToList();
            List<Message> allMessages = FetchMessages();
            List<ResponseMessage> filteredMessages = new List<ResponseMessage>();
            foreach (var message in allMessages)
            {
                if (followingsIds.Contains(message.ComposerId) || message.ComposerId == userId)
                {
                    filteredMessages.Add(new ResponseMessage() 
                    {
                        MessageId = message.MessageId,
                        ComposerId = message.ComposerId,
                        ReplyToId = message.ReplyToId,
                        Text = message.Text,
                        ComposerName = message.ComposerName,
                        DateTime = message.DateTime,
                    });
                }
            }
            return filteredMessages;
        }

        public List<ResponseMessage> FetchFriendsMessages(string username)
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

        public User GetCurrentUser(string username)
            => _unitOfWork.Users.GetByUsername(username);

    }
}
