using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebServer.Models.DBModels;
using WebServer.Models.RequestModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Interfaces
{
    public interface IAuthenticationService
    {
        Response<UserModel> AuthenticateUser(LoginRequest user);
        Response<UserModel> SignUpUser(User newUser);
        string GenerateJSONWebToken(int id, string username);
        string GetClaim(ClaimsPrincipal principal, string type);
    }
}
