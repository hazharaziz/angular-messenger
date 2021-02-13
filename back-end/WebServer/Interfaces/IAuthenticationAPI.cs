using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.DBModels;
using WebServer.Models.RequestModels;

namespace WebServer.Interfaces
{
    public interface IAuthenticationAPI
    {
        User AuthenticateUser(LoginRequest user);
        User SignUpUser(User newUser);
        string GenerateJSONWebToken(User user);
    }
}
