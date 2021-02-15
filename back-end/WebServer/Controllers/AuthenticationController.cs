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
        private IUnitOfWork _unitOfWork;
        private IAuthenticationAPI _auth;

        public AuthenticationController(IAuthenticationAPI auth, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _auth = auth;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            try
            {
                Response<Authentication> response = _auth.AuthenticateUser(login);
                response.Token = _auth.GenerateJSONWebToken(response.Data.Id.ToString(), response.Data.Username);
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
                Response<Authentication> response = _auth.SignUpUser(newUser);
                response.Token = _auth.GenerateJSONWebToken(response.Data.Id.ToString(), response.Data.Username);
                return StatusCode(response.Status, response);
            }
            catch (HttpException exception)
            {
                return StatusCode(exception.Status, new { message = exception.Message });
            }

        }

    }
}
