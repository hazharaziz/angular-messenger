using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet("{id}/followers")]
        public ActionResult<IEnumerable<User>> GetFollowers(int id)
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
                return _relations.GetFollowers(id);
            }
            return Unauthorized();
        }
    }
}
