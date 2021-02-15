using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.DBModels;
using WebServer.Models.RequestModels;
using WebServer.Models.ResponseModels;

namespace WebServer.Interfaces
{
    public interface IAuthenticationAPI
    {
        Response<User> AuthenticateUser(LoginRequest user);
        User SignUpUser(User newUser);
        string GenerateJSONWebToken(User user);
    }
}
