using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebServer.Exceptions;
using WebServer.Interfaces;
using WebServer.Messages;
using WebServer.Models.DBModels;
using WebServer.Models.RequestModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Controllers
{
    [Route("api/directs")]
    [ApiController]
    public class DirectController : ControllerBase
    {
        private IAuthenticationService _authService;
        private IDirectService _directService;

        public DirectController(IAuthenticationService authService, IDirectService directService)
        {
            _authService = authService;
            _directService = directService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUserDirects()
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<List<DirectModel>> response = _directService.GetUserDirects(userId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpGet("{directId}")]
        public IActionResult GetDirectMessages(int directId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<List<DirectMessage>> response = _directService.GetDirectMessages(userId, directId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpPost("message")]
        public IActionResult SendDirectMessage([FromBody] DirectMessageRequest directMessage)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                int targetId = directMessage.TargetId;
                DirectMessage message = new DirectMessage()
                {
                    DirectId = directMessage.DirectId,
                    Text = directMessage.Text,
                    ComposerName = directMessage.ComposerName,
                    DateTime = directMessage.DateTime,
                    ReplyToId = directMessage.ReplyToId
                };
                Response<string> response = _directService.SendDirectMessage(userId, targetId, message);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpPut("message")]
        public IActionResult EditDirectMessage([FromBody] DirectMessage message)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _directService.EditDirectMessage(userId, message.DirectMessageId, message);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpDelete("message/{messageId}")]
        public IActionResult DeleteDirectMessage(int messageId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _directService.DeleteDirectMessage(userId, messageId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpDelete("history/{directId}")]
        public IActionResult DeleteDirectHistory(int directId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _directService.DeleteDirectHistory(userId, directId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

        [Authorize]
        [HttpDelete("{directId}")]
        public IActionResult DeleteDirect(int directId)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<string> response = _directService.DeleteDirect(userId, directId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, Alerts.SomethingWentWrong);
            }
        }

    }
}
