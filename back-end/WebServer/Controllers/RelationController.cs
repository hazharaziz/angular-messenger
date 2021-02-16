using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using WebServer.Exceptions;
using WebServer.Interfaces;
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Controllers
{
    [Route("api/relations")]
    [ApiController]
    public class RelationController : ControllerBase
    {
        private IAuthenticationService _authService;
        private IRelationService _relationService;

        public RelationController(IAuthenticationService authService, IRelationService relationService)
        {
            _authService = authService;
            _relationService = relationService;
        }

        [Authorize]
        [HttpGet("followers")]
        public IActionResult GetFollowers()
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<List<UserModel>> response = _relationService.GetFollowers(int.Parse(userId));
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception) 
            {
                return StatusCode(exception.Status, new { message = exception.Message });
            }
        }

        [Authorize]
        [HttpGet("followings")]
        public IActionResult GetFollowings()
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<List<UserModel>> response = _relationService.GetFollowings(int.Parse(userId));
                return StatusCode(StatusCodes.Status200OK, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, new { message = exception.Message });
            }
        }

        [Authorize]
        [HttpGet("requests")]
        public IActionResult GetFollowRequests()
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<List<UserModel>> response = _relationService.GetFollowRequests(int.Parse(userId));
                return StatusCode(StatusCodes.Status200OK, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, new { message = exception.Message });
            }
        }

        [Authorize]
        [HttpPost("send-request/{id}")]
        public IActionResult SendFollowRequest(int id)
        {            
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);                
                Response<string> response = _relationService.SendFollowRequest(id, int.Parse(userId));
                return StatusCode(response.Status, new { message = response.Data });
            } 
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, new { message = exception.Message });
            }
        }

        [Authorize]
        [HttpPut("accept-request/{id}")]
        public IActionResult AcceptFollowRequest(int id)
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<string> response = _relationService.AcceptFollowRequest(int.Parse(userId), id);
                return StatusCode(response.Status, new { message = response.Data });
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, new { message = exception.Message });
            }
        }

        [Authorize]
        [HttpDelete("reject-request/{id}")]
        public IActionResult RejectFollowRequest(int id)
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<string> response = _relationService.RejectFollowRequest(int.Parse(userId), id);
                return StatusCode(response.Status, new { message = response.Data });
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, new { message = exception.Message });
            }
        }

        [Authorize]
        [HttpDelete("cancel-request/{id}")]
        public IActionResult CancelFollowRequest(int id)
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<string> response = _relationService.CancelRequest(id, int.Parse(userId));
                return StatusCode(response.Status, new { message = response.Data });
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, new { message = exception.Message });
            }
        }

        [Authorize]
        [HttpDelete("unfollow/{id}")]
        public IActionResult Unfollow(int id)
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<string> response = _relationService.Unfollow(id, int.Parse(userId));
                return StatusCode(response.Status, new { message = response.Data });
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, new { message = exception.Message });
            }
        }
    }
}
