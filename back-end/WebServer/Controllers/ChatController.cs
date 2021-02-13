using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebServer.Models.DBModels;
using WebServer.Interfaces;
using WebServer.Models.ResponseModels;

namespace WebServer.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IChatAPI _chat;

        public ChatController(IUnitOfWork unitOfWork, IChatAPI chat)
        {
            _unitOfWork = unitOfWork;
            _chat = chat;
        }

        [Authorize]
        [HttpGet("messages")]
        public ActionResult<IEnumerable<ResponseMessage>> Get()
        {
            var currentUser = HttpContext.User;
            string username = "";
            if (currentUser.HasClaim(claim => claim.Type == ClaimTypes.Name))
            {
                username = currentUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name).Value;
            }
            if (username != "")
            {
                return _chat.FetchFriendsMessages(username);
            }
            return Unauthorized();
        }
    }
}
