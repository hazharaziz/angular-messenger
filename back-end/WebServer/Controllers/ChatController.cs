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
using WebServer.Exceptions;
using WebServer.Models.ResponseModels;

namespace WebServer.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private IChatService _chatService;
        private IAuthenticationService _authService;

        public ChatController(IAuthenticationService authService, IChatService chatService)
        {
            _authService = authService;
            _chatService = chatService;
        }

        [Authorize]
        public IActionResult GetMessages()
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<List<Message>> response = _chatService.FetchFriendsMessages(int.Parse(userId));
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, new { message = exception.Message });
            }
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
                    _chatService.AddMessage(message);
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
                    _chatService.EditMessage(id, message);
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
                    _chatService.DeleteMessage(id);
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
