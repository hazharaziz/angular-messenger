using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.DataContext;
using WebServer.Interfaces;
using WebServer.Models;

namespace WebServer.Services
{
    public class AuthenticationAPI : IAuthenticationAPI
    {
        private MessengerContext _context;
        
        public AuthenticationAPI(MessengerContext context)
        {
            _context = context;
        }

        public void LoginUser(User user)
        {
            throw new NotImplementedException();
        }

        public void LogoutUser()
        {
            throw new NotImplementedException();
        }

        public void SignUpUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
