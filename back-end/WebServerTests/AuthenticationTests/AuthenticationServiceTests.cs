using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebServer.Exceptions;
using WebServer.Interfaces;
using WebServer.Messages;
using WebServer.Models.DBModels;
using WebServer.Models.RequestModels;
using WebServer.Models.ResponseModels;
using WebServer.Services;
using WebServerTests.TestData;
using Xunit;

namespace WebServerTests
{
    public class AuthenticationServiceTests
    {
        private readonly AuthenticationService _authService;
        private readonly TokenValidationParameters _parameters;
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IConfiguration> _config = new Mock<IConfiguration>();

        public AuthenticationServiceTests()
        {
            _authService = new AuthenticationService(_config.Object, _unitOfWork.Object);
            _parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "test.com",
                ValidAudience = "test.com",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisismySecretKey"))
            };
        }

        [Fact]
        public void SignUpUser_ReturnsUserModelResponse()
        {
            // Arange
            User newUser = Users.Patrick;
            _unitOfWork.SetupSequence(u => u.Users.GetByUsername(It.IsAny<string>()))
                .Returns(Users.Null)
                .Returns(Users.Patrick);

            _unitOfWork.Setup(u => u.Users.Add(It.IsAny<User>()));
            _unitOfWork.Setup(u => u.Save());

            Response<UserModel> expected = new Response<UserModel>()
            {
                Status = StatusCodes.Status201Created,
                Data = UserModels.Patrick
            };

            // Act
            Response<UserModel> response = _authService.SignUpUser(Users.Patrick);

            // Assert
            Assert.Equal(expected.Status, response.Status);
            Assert.Equal(expected.Data.Id, response.Data.Id);
            Assert.Equal(expected.Data.Username, response.Data.Username);
            Assert.Equal(expected.Data.Name, response.Data.Name);
            Assert.Equal(expected.Data.IsPublic, response.Data.IsPublic);
        }

        [Fact]
        public void SignUpUser_ThrowsHttpException_Status409Conflict()
        {
            // Arrange
            User newUser = Users.Adam;
            _unitOfWork.SetupSequence(u => u.Users.GetByUsername(It.IsAny<string>()))
                .Returns(Users.Adam)
                .Returns(Users.Adam);

            // Act
            Action action = () => _authService.SignUpUser(newUser);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void AuthenticateUser_ReturnsUserModelResponse()
        {
            // Arrange
            LoginRequest user = LoginRequests.Adam;
            _unitOfWork.Setup(u => u.Users.GetByUsername(user.Username))
                       .Returns(Users.Adam);

            Response<UserModel> expected = new Response<UserModel>()
            {
                Status = StatusCodes.Status200OK,
                Data = UserModels.Adam
            };

            // Act
            var response = _authService.AuthenticateUser(user);

            // Assert
            Assert.Equal(expected.Status, response.Status);
            Assert.Equal(expected.Data.Id, response.Data.Id);
            Assert.Equal(expected.Data.Username, response.Data.Username);
            Assert.Equal(expected.Data.Name, response.Data.Name);
            Assert.Equal(expected.Data.IsPublic, response.Data.IsPublic);
        }

        [Fact]
        public void AuthenticateUser_ThrowsHttpException_Status404NotFound()
        {
            // Arrange
            LoginRequest user = LoginRequests.Edward;
            _unitOfWork.Setup(u => u.Users.GetByUsername(user.Username))
                       .Returns(Users.Null);
            
            // Act
            Action action = () => _authService.AuthenticateUser(user);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void AuthenticateUser_ThrowsHttpException_Status401Unauthorized()
        {
            // Arrange
            LoginRequest user = LoginRequests.Isaac;
            user.Password = "1234";
            _unitOfWork.Setup(u => u.Users.GetByUsername(user.Username))
                       .Returns(Users.Isaac);
            var expected = new HttpException
                        (StatusCodes.Status401Unauthorized, Alerts.WrongPassword);

            // Act
            Action action = () => _authService.AuthenticateUser(user);

            // Assert
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GenerateJSONWebToken_ReturnsToken()
        {
            // Arrange
            User user = Users.Adam;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                       .Returns(Users.Adam);
            _config.Setup(c => c["Jwt:Key"]).Returns("ThisismySecretKey");
            _config.Setup(c => c["Jwt:Issuer"]).Returns("test.com");

            // Act
            var token = _authService.GenerateJSONWebToken(user.Id, user.Username);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var principal = handler.ValidateToken(token, _parameters, 
                out SecurityToken validatedToken);

            string id = _authService.GetClaim(principal, ClaimTypes.NameIdentifier);
            string username = _authService.GetClaim(principal, ClaimTypes.Name);

            // Assert            
            Assert.IsType<string>(token);
            Assert.True(handler.CanReadToken(token));
            Assert.Equal(id, user.Id.ToString());
            Assert.Equal(username, user.Username);
        }

        [Fact]
        public void GenerateJSONWebToken_ThrowsHttpException_Status404Unauthorized()
        {
            // Arrange
            User user = Users.Adam;
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                       .Returns(Users.Null);

            // Act
            Action action = () => _authService.GenerateJSONWebToken(user.Id, user.Username);

            // Assert            
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GenerateJSONWebToken_ThrowsHttpException_Status403Forbidden()
        {
            // Arrange
            _unitOfWork.Setup(u => u.Users.Get(It.IsAny<int>()))
                       .Returns(Users.Isaac);

            // Act
            // user id doesn't match the user username
            Action action = () => _authService.GenerateJSONWebToken(1, Users.Adam.Username);

            // Assert            
            Assert.Throws<HttpException>(action);
        }

        [Fact]
        public void GetClaim_ReturnsString()
        {
            // Arrange
            ClaimsIdentity identity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, Users.Adam.Username),
                new Claim(ClaimTypes.NameIdentifier, Users.Adam.Id.ToString())
            });
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // Act
            var usernameClaim = _authService.GetClaim(principal, ClaimTypes.Name);
            var idClaim = _authService.GetClaim(principal, ClaimTypes.NameIdentifier);

            // Assert
            Assert.Equal(Users.Adam.Username, usernameClaim);
            Assert.False(Users.Adam.Id.ToString() != idClaim);
        }

        [Fact]
        public void GetClaim_ThrowsHttpException_Status401Unauthorized()
        {
            // Arrange
            ClaimsIdentity identity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, Users.Adam.Username),
            });
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // Act
            Action action = () => _authService.GetClaim(principal, ClaimTypes.NameIdentifier);

            // Assert
            Assert.Throws<HttpException>(action);
        }
    }
}
