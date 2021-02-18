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
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            List<Direct> directs = _unitOfWork.Directs.GetDirects(userId);
            List<DirectModel> directModels = new List<DirectModel>();
            foreach (Direct direct in directs)
            {
                int targetId = (direct.FirstUserId == userId) ? direct.SecondUserId : direct.FirstUserId;
                User target = _unitOfWork.Users.Get(targetId);
                directModels.Add(new DirectModel()
                {
                    Id = direct.DirectId,
                    DirectName = (target != null) ? target.Name : "Deleted Account"
                });
            }

            return new Response<List<DirectModel>>()
            {
                Status = StatusCodes.Status200OK,
                Data = directModels
            };
        }

        public Response<List<DirectMessage>> GetDirectMessages(int directId)
        {
            if (_unitOfWork.Directs.Get(directId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.DirectNotFound);

            List<DirectMessage> messages = _unitOfWork.DirectMessages.GetDirectMessages(directId)
                                            .OrderByDescending(d => d.DateTime).ToList();
            return new Response<List<DirectMessage>>()
            {
                Status = StatusCodes.Status200OK,
                Data = messages
            };
        }

        public Response<string> SendDirectMessage(int userId, int targetId, DirectMessage directMessage)
        {
            if (_unitOfWork.Users.Get(userId) == null || _unitOfWork.Users.Get(targetId) == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UsersNotFound);

            Direct direct = _unitOfWork.Directs.Get(directMessage.DirectId);
            if (direct == null)
            {
                _unitOfWork.Directs.Add(new Direct()
                {
                    FirstUserId = userId,
                    SecondUserId = targetId
                });
            }

            direct = _unitOfWork.Directs.Get(userId, targetId);
            directMessage.DirectId = direct.DirectId;
            directMessage.ComposerId = userId;
            _unitOfWork.DirectMessages.Add(directMessage);
            _unitOfWork.Save();

            return new Response<string>()   
            {
                Status = StatusCodes.Status201Created,
                Data = Alerts.DirectMessageCreated
            };
        }

        public Response<string> EditDirectMessage(int directMessageId, DirectMessage editedMessage)
        {
            throw new NotImplementedException();
        }
        public Response<string> DeleteDirect(int directId)
        {
            throw new NotImplementedException();
        }

        public Response<string> DeleteDirectHistory(int directId)
        {
            throw new NotImplementedException();
        }

        public Response<string> DeleteDirectMessage(int directMessageId)
        {
            throw new NotImplementedException();
        }
    }
}
