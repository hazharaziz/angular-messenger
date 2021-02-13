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

namespace WebServer.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase, IAuthenticationAPI
    {
        private IConfiguration _config;
        private IUnitOfWork _unitOfWork;
        public AuthenticationController(IConfiguration config, IUnitOfWork unitOfWork)
        {
            _config = config;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            IActionResult response = Unauthorized();
            User user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken();
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
                var tokenString = GenerateJSONWebToken();
                return Ok(new
                {
                    token = tokenString,
                    name = $"{newUser.FirstName} {newUser.LastName}",
                    newUser.Username,
                });
            }
            return Conflict();            
        }

        public User AuthenticateUser(LoginRequest user)
        {
            User dbUser = _unitOfWork.Users.Find(u => u.Username == user.Username).FirstOrDefault();
            User result = null;

            if (dbUser != null)
                result = (dbUser.Password == user.Password) ? dbUser : result;

            return result;
        }

        public string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
