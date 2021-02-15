using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.DBModels;
using WebServer.Interfaces;
using WebServer.Models.RequestModels;
using WebServer.Models.ResponseModels;
using WebServer.Exceptions;
using WebServer.Messages;
using Microsoft.AspNetCore.Http;

namespace WebServer.Services
{
    public class Authentication : IAuthenticationAPI
    {
        private IConfiguration _config;
        private IUnitOfWork _unitOfWork;

        public Authentication(IConfiguration config, IUnitOfWork unitOfWork)
        {
            _config = config;
            _unitOfWork = unitOfWork;
        }

        public Response<User> AuthenticateUser(LoginRequest user)
        {
            User dbUser = _unitOfWork.Users.Find(u => u.Username == user.Username).FirstOrDefault();

            if (dbUser == null) 
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.NotFound);
            
            if (dbUser.Password != user.Password) 
                throw new HttpException(StatusCodes.Status401Unauthorized, Alerts.UnAuthorized);

            return new Response<User>()
            {
                Status = StatusCodes.Status200OK,
                Data = dbUser,
            };
        }

        public User SignUpUser(User newUser)
        {
            _unitOfWork.Users.Add(newUser);
            _unitOfWork.Save();
            return _unitOfWork.Users.Find(u => u.Username == newUser.Username).FirstOrDefault();
        }

        public string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.Name, (user != null) ? user.Username : ""),
                new Claim(ClaimTypes.NameIdentifier, (user != null) ? user.Id.ToString() : "")
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
