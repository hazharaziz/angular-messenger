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
using WebServer.DBModels.Models;
using WebServer.Models.RequestModels;
using System.Security.Claims;

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
            IActionResult response = Unauthorized();
            User user = _auth.AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = _auth.GenerateJSONWebToken(user);
                response = Ok(new
                {
                    Token = tokenString,
                    Name = $"{user.FirstName} {user.LastName}"
                });
            }

            return response;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] User newUser)
        {
            if (_unitOfWork.Users.Find(user => user.Username == newUser.Username) == null)
            {
                _unitOfWork.Users.Add(newUser);
                _unitOfWork.Save();
                var tokenString = _auth.GenerateJSONWebToken(newUser);
                return Ok(new
                {
                    token = tokenString,
                    name = $"{newUser.FirstName} {newUser.LastName}",
                    newUser.Username,
                });
            }
            return Conflict();
        }

    }
}
