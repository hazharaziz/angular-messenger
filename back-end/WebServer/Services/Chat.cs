using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Interfaces;
using WebServer.Models.DBModels;

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

        public List<Message> FetchFriendsMessages(int userId)
        {
            List<int> followingsIds = _relationAPI.GetFollowings(userId).Select(f => f.Id).ToList();
            List<Message> allMessages = FetchMessages();
            List<Message> filteredMessages = new List<Message>();
            foreach (var message in allMessages)
            {
                if (followingsIds.Contains(message.ComposerId) || message.ComposerId == userId)
                {
                    filteredMessages.Add(message);
                }
            }
            return filteredMessages.OrderByDescending(m => m.DateTime).ToList();
        }

        public List<Message> FetchFriendsMessages(string username)
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
