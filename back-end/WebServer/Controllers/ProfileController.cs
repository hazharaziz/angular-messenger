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
using WebServer.Models.ResponseModels;

namespace WebServer.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private IAuthenticationService _authService;
        private IProfileService _profileService;

        public ProfileController(IAuthenticationService authService, IProfileService profileService)
        {
            _authService = authService;
            _profileService = profileService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetProfile()
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<UserModel> response = _profileService.GetProfile(int.Parse(userId));
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, new { message = exception.Message });
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult EditProfile([FromBody] UserModel editedUser)
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<UserModel> response = _profileService.EditProfile(int.Parse(userId), editedUser);
                return StatusCode(response.Status, response.Data);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, new { message = exception.Message });
            }
        }

    }
}
