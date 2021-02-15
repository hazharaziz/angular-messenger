using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.DataContext;
using WebServer.Interfaces;
using WebServer.Models.RequestModels;
using System.Security.Claims;
using WebServer.Models.DBModels;
using WebServer.Models.ResponseModels;
using WebServer.Exceptions;

namespace WebServer.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            try
            {
                Response<Authentication> response = _authService.AuthenticateUser(login);
                response.Token = _authService.GenerateJSONWebToken(response.Data.Id.ToString(), response.Data.Username);
                return StatusCode(response.Status, response);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, new { message = exception.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] User newUser)
        {
            try
            {
                Response<Authentication> response = _authService.SignUpUser(newUser);
                response.Token = _authService.GenerateJSONWebToken(response.Data.Id.ToString(), response.Data.Username);
                return StatusCode(response.Status, response);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, new { message = exception.Message });
            }

        }

    }
}
