using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using WebServer.Interfaces;
using WebServer.Models.DBModels;

namespace WebServer.Controllers
{
    [Route("api/relations")]
    [ApiController]
    public class RelationController : ControllerBase
    {
        private IRelationAPI _relations;

        public RelationController(IRelationAPI relations)
        {
            _relations = relations;
        }

        [Authorize]
        [HttpGet("followers")]
        public ActionResult<IEnumerable<User>> GetFollowers()
        {
            IActionResult response = Unauthorized();
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
            return _relations.GetFollowers(int.Parse(userId));
        }

        [Authorize]
        [HttpGet("followings")]
        public ActionResult<IEnumerable<User>> GetFollowings()
        {
            IActionResult response = Unauthorized();
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
            return _relations.GetFollowings(int.Parse(userId));
        }


        [Authorize]
        [HttpGet("requests")]
        public ActionResult<IEnumerable<User>> GetFollowRequests()
        {
            IActionResult response = Unauthorized();
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
            return _relations.GetFollowRequests(int.Parse(userId));
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
                
                _relations.SendFollowRequest(id, int.Parse(userId));
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
                _relations.AcceptFollowRequest(int.Parse(userId), id);
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
                _relations.RejectFollowRequest(int.Parse(userId), id);
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
                _relations.CancelRequest(id, int.Parse(userId));
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
                _relations.Unfollow(id, int.Parse(userId));
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
