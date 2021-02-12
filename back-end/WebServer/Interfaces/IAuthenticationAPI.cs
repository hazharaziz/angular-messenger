﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models;

namespace WebServer.Interfaces
{
    public interface IAuthenticationAPI
    {
        void AuthenticateUser(User user);
        void SignUpUser(User user);
        void LogoutUser();
    }
}
