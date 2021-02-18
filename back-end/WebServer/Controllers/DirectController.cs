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
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<List<DirectModel>> response = _directService.GetUserDirects(int.Parse(userId));
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [Authorize]
        [HttpGet("{directId}")]
        public IActionResult GetDirectMessages(int directId)
        {
            try
            {
                Response<List<DirectMessage>> response = _directService.GetDirectMessages(directId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult SendDirectMessage([FromBody] DirectMessageRequest directMessage)
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                int targetId = directMessage.TargetId;
                DirectMessage message = new DirectMessage()
                {
                    DirectId = directMessage.DirectId,
                    Text = directMessage.Text,
                    ComposerName = directMessage.ComposerName,
                    DateTime = directMessage.DateTime,
                    ReplyToId = directMessage.ReplyToId
                };
                Response<string> response = _directService.SendDirectMessage(int.Parse(userId), targetId, message);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult EditDirectMessage([FromBody] DirectMessage message)
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<string> response = _directService.EditDirectMessage(message.DirectMessageId, message);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [Authorize]
        [HttpDelete("dm/{messageId}")]
        public IActionResult DeleteDirectMessage(int messageId)
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<string> response = _directService.DeleteDirectMessage(int.Parse(userId), messageId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [Authorize]
        [HttpDelete("history/{directId}")]
        public IActionResult DeleteDirectHistory(int directId)
        {
            try
            {
                Response<string> response = _directService.DeleteDirectHistory(directId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }


        [Authorize]
        [HttpDelete("{directId}")]
        public IActionResult DeleteDirect(int directId)
        {
            try
            {
                Response<string> response = _directService.DeleteDirect(directId);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }


    }
}
