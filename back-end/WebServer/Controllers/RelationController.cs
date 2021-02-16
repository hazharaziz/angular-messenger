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
            var currentUser = HttpContext.User;
            string username = "";
            string userId = "0";
            if (currentUser.HasClaim(claim => claim.Type == ClaimTypes.Name))
            {
                username = currentUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name).Value;
            }
            if (currentUser.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier))
            {
                userId = currentUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            }

            if (userId == null || username == null)
            {
                return Unauthorized();
            }
            
            try
            {
                
                _relationService.SendFollowRequest(id, int.Parse(userId));
                return StatusCode(201, new
                {
                    message = "Follow request successfully sent"
                });
            } 
            catch (Exception)
            {
                return StatusCode(500, new 
                {
                    message = "An error has occured in the server"
                });
            }
        }

        [Authorize]
        [HttpPut("accept-request/{id}")]
        public IActionResult AcceptFollowRequest(int id)
        {
            var currentUser = HttpContext.User;
            string username = "";
            string userId = "0";
            if (currentUser.HasClaim(claim => claim.Type == ClaimTypes.Name))
            {
                username = currentUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name).Value;
            }
            if (currentUser.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier))
            {
                userId = currentUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            }

            if (userId == null || username == null)
            {
                return Unauthorized();
            }

            try
            {
                _relationService.AcceptFollowRequest(int.Parse(userId), id);
                return StatusCode(200, new
                {
                    message = "Follow request accepted successfully"
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An error has occured in the server"
                });
            }
        }


        [Authorize]
        [HttpDelete("reject-request/{id}")]
        public IActionResult DeleteFollowRequest(int id)
        {
            var currentUser = HttpContext.User;
            string username = "";
            string userId = "0";
            if (currentUser.HasClaim(claim => claim.Type == ClaimTypes.Name))
            {
                username = currentUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name).Value;
            }
            if (currentUser.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier))
            {
                userId = currentUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            }

            if (userId == null || username == null)
            {
                return Unauthorized();
            }

            try
            {
                _relationService.RejectFollowRequest(int.Parse(userId), id);
                return StatusCode(200, new
                {
                    message = "Follow request rejected successfully"
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An error has occured in the server"
                });
            }
        }

        [Authorize]
        [HttpDelete("cancel-request/{id}")]
        public IActionResult CancelFollowRequest(int id)
        {
            var currentUser = HttpContext.User;
            string userId = "0";
            if (currentUser.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier))
            {
                userId = currentUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            }

            if (userId == null)
            {
                return Unauthorized();
            }

            try
            {
                _relationService.CancelRequest(id, int.Parse(userId));
                return StatusCode(200, new
                {
                    message = "Follow request canceled successfully"
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An error has occured in the server"
                });
            }
        }

        [Authorize]
        [HttpDelete("unfollow/{id}")]
        public IActionResult Unfollow(int id)
        {
            var currentUser = HttpContext.User;
            string userId = "0";
            if (currentUser.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier))
            {
                userId = currentUser.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            }

            if (userId == null)
            {
                return Unauthorized();
            }

            try
            {
                _relationService.Unfollow(id, int.Parse(userId));
                return StatusCode(200, new
                {
                    message = "you unfollowed the user successfully"
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An error has occured in the server"
                });
            }
        }
    }
}
