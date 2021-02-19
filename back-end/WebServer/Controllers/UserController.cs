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
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<List<UserModel>> response = _userService.GetAllUsers(int.Parse(userId));
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
        [HttpGet("{text}")]
        public IActionResult FilterUsers(string text)
        {
            try
            {
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<List<UserModel>> response = _userService.FilterUsers(int.Parse(userId), text);
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
                var principal = HttpContext.User;
                string userId = _authService.GetPrincipalClaim(principal, ClaimTypes.NameIdentifier);
                Response<List<UserModel>> response = _userService.GetUserFriends(int.Parse(userId));
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
