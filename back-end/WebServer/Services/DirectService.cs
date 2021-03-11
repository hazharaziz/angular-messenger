using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Exceptions;
using WebServer.Interfaces;
using WebServer.Messages;
using WebServer.Models.DBModels;
using WebServer.Models.RequestModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Services
{
    public class DirectService : IDirectService
    {
        private IUnitOfWork _unitOfWork;

        public DirectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Response<List<DirectModel>> GetUserDirects(int userId)
        {
            if (_unitOfWork.Users.Get(userId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            List<DirectModel> directModels = new List<DirectModel>();
            _unitOfWork.Directs.GetDirects(userId).ForEach(direct =>
            {
                int targetId = (direct.FirstUserId == userId) ? direct.SecondUserId : direct.FirstUserId;
                User target = _unitOfWork.Users.Get(targetId);
                directModels.Add(new DirectModel()
                {
                    Id = direct.DirectId,
                    TargetId = targetId,
                    TargetName = (target != null) ? target.Name : "Deleted Account"
                });
            });

            return new Response<List<DirectModel>>()
            {
                Status = StatusCodes.Status200OK,
                Data = directModels
            };
        }

        public Response<List<DirectMessage>> GetDirectMessages(int userId, int targetId)
        {
            Direct direct = _unitOfWork.Directs.Get(userId, targetId);
            if (direct == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.DirectNotFound);

            if (direct.FirstUserId != userId && direct.SecondUserId != userId)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            List<DirectMessage> messages = _unitOfWork.DirectMessages.GetDirectMessages(direct.DirectId)
                                            .OrderByDescending(d => d.DateTime).ToList();
            return new Response<List<DirectMessage>>()
            {
                Status = StatusCodes.Status200OK,
                Data = messages
            };
        }

        public Response<string> SendDirectMessage(int userId, int targetId, DirectMessage directMessage)
        {
            User user = _unitOfWork.Users.Get(userId);
            if (user == null || _unitOfWork.Users.Get(targetId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            Direct direct = _unitOfWork.Directs.Get(userId, targetId);
            if (direct == null)
            {
                _unitOfWork.Directs.Add(new Direct()
                {
                    FirstUserId = userId,
                    SecondUserId = targetId
                });
                _unitOfWork.Save();
            } 

            direct = _unitOfWork.Directs.Get(userId, targetId);
            directMessage.DirectId = direct.DirectId;
            directMessage.ComposerId = user.Id;
            directMessage.ComposerName = user.Name;
            _unitOfWork.DirectMessages.Add(directMessage);
            _unitOfWork.Save();

            return new Response<string>()   
            {
                Status = StatusCodes.Status201Created,
                Data = Alerts.MessageCreated
            };
        }

        public Response<string> EditDirectMessage(int userId, int directId, int directMessageId, DirectMessage editedMessage)
        {
            if (_unitOfWork.Users.Get(userId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            DirectMessage message = _unitOfWork.DirectMessages.Get(directMessageId);
            if (message == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.MessageNotFound);

            if (userId != message.ComposerId)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);
            
            if (message.DirectId != directId)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            message.Text = editedMessage.Text;
            _unitOfWork.Save();
            return new Response<string>()
            {
                Status = StatusCodes.Status200OK,
                Data = Alerts.MessageEdited
            };
        }

        public Response<string> DeleteDirectMessage(int userId, int directId, int directMessageId)
        {
            if (_unitOfWork.Users.Get(userId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            DirectMessage directMessage = _unitOfWork.DirectMessages.Get(directMessageId);
            if (directMessage == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.MessageNotFound);

            if (directMessage.ComposerId != userId)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            if (directId != directMessage.DirectId)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            _unitOfWork.DirectMessages.Remove(directMessage);
            _unitOfWork.Save();

            return new Response<string>()
            {
                Status = StatusCodes.Status200OK,
                Data = Alerts.MessageDeleted
            };
        }

        public Response<string> DeleteDirectHistory(int userId, int targetId)
        {
            if (_unitOfWork.Users.Get(userId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            Direct direct = _unitOfWork.Directs.Get(userId, targetId);
            if (direct == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.DirectNotFound);

            if (direct.FirstUserId != userId && direct.SecondUserId != userId)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            _unitOfWork.DirectMessages.GetAll().ForEach(message =>
            {
                if (message.DirectId == direct.DirectId)
                    _unitOfWork.DirectMessages.Remove(message);
            });
            _unitOfWork.Save();

            return new Response<string>()
            {
                Status = StatusCodes.Status200OK,
                Data = Alerts.HistoryDeleted
            };
        }

        public Response<string> DeleteDirect(int userId, int directId)
        {
            if (_unitOfWork.Users.Get(userId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            Direct direct = _unitOfWork.Directs.Get(directId);
            if (direct == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.DirectNotFound);

            if (direct.FirstUserId != userId && direct.SecondUserId != userId)
                throw new HttpException(StatusCodes.Status405MethodNotAllowed, Alerts.NotAllowed);

            _unitOfWork.Directs.Remove(direct);
            _unitOfWork.Save();
            return new Response<string>()
            {
                Status = StatusCodes.Status200OK,
                Data = Alerts.DirectDeleted
            };
        }

    }
}
