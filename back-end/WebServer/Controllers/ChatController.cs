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
        public ActionResult<IEnumerable<Message>> GetMessages()
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

        [Authorize]
        [HttpPost]
        public IActionResult SendMessage([FromBody] Message message)
        {
            IActionResult response = Unauthorized();
            var currentUser = HttpContext.User;
            string username = "";
            if (currentUser.HasClaim(claim => claim.Type == ClaimTypes.Name))
            {
                username = currentUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name).Value;
            }
            if (username != "")
            {
                try
                {
                    _chat.AddMessage(message);
                    return Created("message created", message);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return response;
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult EditMessage(int id, [FromBody] Message message)
        {
            IActionResult response = Unauthorized();
            var currentUser = HttpContext.User;
            string username = "";
            if (currentUser.HasClaim(claim => claim.Type == ClaimTypes.Name))
            {
                username = currentUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name).Value;
            }
            if (username != "")
            {
                try
                {
                    _chat.EditMessage(id, message);
                    return Ok();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return response;
        }

        [Authorize]
        [HttpDelete("{id}")]    
        public IActionResult DeleteMessage(int id)
        {
            IActionResult response = Unauthorized();
            var currentUser = HttpContext.User;
            string username = "";
            if (currentUser.HasClaim(claim => claim.Type == ClaimTypes.Name))
            {
                username = currentUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name).Value;
            }
            if (username != "")
            {
                try
                {
                    _chat.DeleteMessage(id);
                    return Ok();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return response;
        }

    }
}
