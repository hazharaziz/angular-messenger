using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.DBModels.Models;
using WebServer.Models.RequestModels;

namespace WebServer.Interfaces
{
    public interface IAuthenticationAPI
    {
        User AuthenticateUser(LoginRequest user);
        string GenerateJSONWebToken();
    }
}
