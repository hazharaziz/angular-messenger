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
using WebServer.Messages;

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
        [HttpGet]
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
                return StatusCode(exception.Status, exception.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult SendMessage([FromBody] Message message)
        {
            try
            {
                Response<string> response = _chatService.AddMessage(message);
                return StatusCode(response.Status, response.Data);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Alerts.InternalServerError);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult EditMessage(int id, [FromBody] Message message)
        {
            try
            {
                Response<string> response = _chatService.EditMessage(id, message);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Alerts.InternalServerError);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]    
        public IActionResult DeleteMessage(int id)
        {
            try
            {
                Response<string> response = _chatService.DeleteMessage(id);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Alerts.InternalServerError);
            }
        }

    }
}
