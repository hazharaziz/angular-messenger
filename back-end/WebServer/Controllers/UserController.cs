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
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IAuthenticationService _authService;
        private IUserService _userService;

        public UserController(IAuthenticationService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet("{text}")]
        public IActionResult FilterUsers(string text)
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<List<UserModel>> response = _userService.FilterUsers(userId, text);
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
        [HttpGet("friends")]
        public IActionResult GetUserFriends()
        {
            try
            {
                int userId = int.Parse(_authService.GetClaim(HttpContext.User, ClaimTypes.NameIdentifier));
                Response<List<UserModel>> response = _userService.GetUserFriends(userId);
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
