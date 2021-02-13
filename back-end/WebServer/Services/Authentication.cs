﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebServer.DBModels.Models;
using WebServer.Interfaces;
using WebServer.Models.RequestModels;

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

        public User AuthenticateUser(LoginRequest user)
        {
            User dbUser = _unitOfWork.Users.Find(u => u.Username == user.Username).FirstOrDefault();
            User result = null;

            if (dbUser != null)
                result = (dbUser.Password == user.Password) ? dbUser : result;

            return result;
        }

        public string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.Name, user.Username),
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