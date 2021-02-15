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
        Response<Authentication> AuthenticateUser(LoginRequest user);
        Response<Authentication> SignUpUser(User newUser);
        string GenerateJSONWebToken(string id, string username);
        string GetPrincipalClaim(ClaimsPrincipal principal, string type);
    }
}
