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
    public class AuthenticationService : IAuthenticationService
    {
        private IConfiguration _config;
        private IUnitOfWork _unitOfWork;

        public AuthenticationService(IConfiguration config, IUnitOfWork unitOfWork)
        {
            _config = config;
            _unitOfWork = unitOfWork;
        }

        public Response<UserModel> AuthenticateUser(LoginRequest user)
        {
            User dbUser = _unitOfWork.Users.GetByUsername(user.Username);

            if (dbUser == null) 
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);
            
            if (dbUser.Password != user.Password) 
                throw new HttpException(StatusCodes.Status401Unauthorized, Alerts.WrongAuthenticationCredentials);

            return new Response<UserModel>()
            {
                Status = StatusCodes.Status200OK,
                Data = new UserModel() 
                {
                    Id = dbUser.Id,
                    Username = dbUser.Username, 
                    Name = dbUser.Name,
                    IsPublic = dbUser.IsPublic
                },
            };
        }

        public Response<UserModel> SignUpUser(User newUser)
        {
            if (_unitOfWork.Users.GetByUsername(newUser.Username) != null)
                throw new HttpException(StatusCodes.Status409Conflict, Alerts.UsernameExists);

            _unitOfWork.Users.Add(newUser);
            _unitOfWork.Save();

            newUser = _unitOfWork.Users.GetByUsername(newUser.Username);

            var authResponse = new UserModel()
            {
                Id = newUser.Id,
                Username = newUser.Username,
                Name = newUser.Name,
                IsPublic = newUser.IsPublic
            };

            return new Response<UserModel>()
            {
                Status = StatusCodes.Status201Created,
                Data = authResponse
            };
        }

        public string GenerateJSONWebToken(int id, string username)
        {
            User user = _unitOfWork.Users.Get(id);
            if (user == null)
                throw new HttpException(StatusCodes.Status404NotFound, Alerts.UserNotFound);

            if (user.Username != username)
                throw new HttpException(StatusCodes.Status403Forbidden, Alerts.Forbidden);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetClaim(ClaimsPrincipal principal, string type)
        {
            string claimValue = (principal.HasClaim(claim => claim.Type == type)) ?
                principal.Claims.FirstOrDefault(claim => claim.Type == type).Value : "";

            if (claimValue == "")
                throw new HttpException(StatusCodes.Status401Unauthorized, Alerts.UnAuthorized);

            return claimValue;
        }
    }
}
